using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagementSystem.Application.Contract.Identity;
using TicketManagementSystem.Application.Models.Identity;

namespace TicketManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("logIn")]
        public async Task<IActionResult> LogIn(AuthenticationRequest loginRequest)
        {
            return Ok(await _authenticationService.AuthenticateAsync(loginRequest));
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest loginRequest)
        {
            return Ok(await _authenticationService.RegisterAsync(loginRequest));
        }
    }
}
