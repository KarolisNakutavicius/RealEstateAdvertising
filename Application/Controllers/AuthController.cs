using Application.DTOs.InputModels;
using Application.DTOs.ViewModels;
using Application.Extensions;
using Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequest request)
        {
            var result = await _authService.Login(request);

            return result.ToHttpResponse();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthenticateRequest request)
        {
            var result = await _authService.Register(request);

            return result.ToHttpResponse();
        }
    }
}
