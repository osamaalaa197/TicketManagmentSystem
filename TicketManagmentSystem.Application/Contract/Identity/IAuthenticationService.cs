using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Models.Identity;

namespace TicketManagementSystem.Application.Contract.Identity
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
        Task<object> RefreshTokenAsync(string token);
        Task<bool> ConfirmationMail(string userId, string token);
        Task LogOut();
        Task<string> GetEmailUserById(string userId);

    }
}
