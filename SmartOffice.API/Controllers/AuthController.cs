using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SmartOffice.Application.DTOs.Auth;
using SmartOffice.Application.Interfaces.Auth;
using SmartOffice.Domain.Entities;

namespace SmartOffice.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                return Ok(await _authService.RegisterAsync(request));
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request) 
        { 
            try
            {
                return Ok(await _authService.LoginAsync(request));
            }
            catch(UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            return Ok(new
            {
                id = User.FindFirstValue("sub"),
                username = User.Identity?.Name,
                role = User.FindFirstValue("role"),
                Permissions = User.FindAll("permission").Select(c => c.Value)
            });
        }
    }
}
