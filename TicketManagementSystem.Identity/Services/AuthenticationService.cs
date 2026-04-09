using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Identity;
using TicketManagementSystem.Application.Contract.Infrastructure;
using TicketManagementSystem.Application.Models.Identity;
using TicketManagementSystem.Identity.Models;

namespace TicketManagementSystem.Identity.Services
{
    public class AuthenticationService:IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IEmailService _emailService;
        private readonly ILogger<ApplicationUser> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> options,IEmailService emailService,ILogger<ApplicationUser> logger, IHttpContextAccessor httpContextAccessor)
        {
            _userManager= userManager;
            _signInManager= signInManager;
            _jwtSettings = options.Value;
            _emailService= emailService;
            _logger= logger;
            _httpContextAccessor= httpContextAccessor;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var user=await _userManager.FindByEmailAsync(request.Email);
            if (user ==null)
                throw new Exception($"User with {request.Email} not found.");
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new Exception($"Credentials for '{request.Email} aren't valid'.");
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
                throw new UnauthorizedAccessException("Email not confirmed.");
            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(user);
            AuthenticationResponse response = new AuthenticationResponse
            {
                Id = user.Id,
                Token = token,
                Email = user.Email,
                UserName = user.UserName,
                RefreshToken = refreshToken.Token

            };
            return response;
        }

        public async Task<object> RefreshTokenAsync(string refreshtoken)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == refreshtoken));
            if (user == null)
                throw new UnauthorizedAccessException("Invalid token");
            var refreshToken = user.RefreshTokens.Single(x => x.Token == refreshtoken);
            if (!refreshToken.IsActive)
                throw new UnauthorizedAccessException("Inactive token");
            var newRefreshToken = GenerateRefreshToken();
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);
            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
            var newOoken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return new
            {
                Token = newOoken,
                RefreshToken = newRefreshToken.Token
            };
        }

        public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
        {
            // Input validation
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Use string.IsNullOrEmpty for better performance over IsNullOrWhiteSpace for usernames
            if (string.IsNullOrEmpty(request.UserName))
                throw new ArgumentException("UserName is required.", nameof(request.UserName));

            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ArgumentException("Email is required.", nameof(request.Email));

            if (string.IsNullOrEmpty(request.Password))
                throw new ArgumentException("Password is required.", nameof(request.Password));

            if (string.IsNullOrWhiteSpace(request.FirstName))
                throw new ArgumentException("FirstName is required.", nameof(request.FirstName));

            if (string.IsNullOrWhiteSpace(request.LastName))
                throw new ArgumentException("LastName is required.", nameof(request.LastName));

            // Email format validation
            if (!IsValidEmail(request.Email))
                throw new ArgumentException("Invalid email format.", nameof(request.Email));

            // Check for existing user
            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
            {
                throw new InvalidOperationException($"Username '{request.UserName}' already exists.");
            }

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail != null)
            {
                throw new InvalidOperationException($"Email '{request.Email}' is already registered.");
            }

            try
            {
                var user = new ApplicationUser
                {
                    Email = request.Email.Trim().ToLowerInvariant(),
                    FirstName = request.FirstName.Trim(),
                    LastName = request.LastName.Trim(),
                    UserName = request.UserName.Trim(),
                    EmailConfirmed = false // Explicitly set to false for new registrations
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    // Generate email confirmation token
                    var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var requestUrl = _httpContextAccessor.HttpContext?.Request;
                    var baseUrl = $"{requestUrl?.Scheme}://{requestUrl?.Host}";
                    var urlEncodedToken = WebUtility.UrlEncode(emailToken);

                    var confirmationLink = $"{baseUrl}/api/account/ConfirmEmail?userId={WebUtility.UrlEncode(user.Id)}&token={urlEncodedToken}";
                    var subject = "Confirm Your Account";
                    var htmlContent = $@"
                                <h2>Welcome to Our Service!</h2>
                                <p>Please confirm your account by clicking the link below:</p>
                                <p><a href='{confirmationLink}' style='background-color: #007bff; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Confirm Email</a></p>
                                <p>Or copy and paste this link in your browser:<br>{confirmationLink}</p>
                                <p><em>This link will expire in 24 hours.</em></p>";

                    var emailSent = await _emailService.SendEmail(new Application.Models.Mail.Email
                    {
                        Body = htmlContent,
                        To = user.Email,
                        Subject = subject,
                    });

                    if (emailSent)
                    {
                        // Log successful registration (consider adding logging)
                        _logger?.LogInformation("User {UserName} registered successfully. Email sent to {Email}",
                            user.UserName, user.Email);

                        return new RegistrationResponse
                        {
                            UserId = user.Id,
                            Message = "Registration successful. Please check your email for a confirmation link.",
                        };
                    }
                    else
                    {
                        _logger?.LogWarning("User {UserName} registered but confirmation email failed to send",
                            user.UserName);

                        return new RegistrationResponse
                        {
                            UserId = user.Id,
                            Message = "Registration successful but we couldn't send the confirmation email. Please contact support.",
                        };
                    }
                }

                // Handle Identity errors
                var errors = result.Errors?.Select(e => e.Description) ?? Enumerable.Empty<string>();
                var errorMessage = errors.Any()
                    ? string.Join(" | ", errors)
                    : "User creation failed for unknown reasons.";

                throw new InvalidOperationException(errorMessage);
            }
            catch (Exception ex) when (ex is not InvalidOperationException && ex is not ArgumentException)
            {
                // Log unexpected exceptions
                _logger?.LogError(ex, "Unexpected error during user registration for email {Email}", request.Email);
                throw new InvalidOperationException("An error occurred during registration. Please try again.");
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
            };
        }

        public async Task<bool> ConfirmationMail(string userId,string token)
        {
            if (userId == null || token == null)
            {
                return false;
            }
            var decodedUserId = WebUtility.UrlDecode(userId);
            var decodedToken = WebUtility.UrlDecode(token);
            var user = await _userManager.FindByIdAsync(decodedUserId);
            if (user == null)
                return false;
         
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (result.Succeeded)
            {
             return true;
            }
            else
            {
                return false;
            }

        }


        public async Task LogOut()
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);
            if (user is not null)
            {
                foreach (var rt in user.RefreshTokens)
                {
                    rt.Revoked = DateTime.UtcNow;
                }
                await _userManager.UpdateAsync(user);
                await _signInManager.SignOutAsync();
            }
        }

        public async Task<string> GetEmailUserById(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                return user.Email;
            }
            return null;
        }
    }
}
