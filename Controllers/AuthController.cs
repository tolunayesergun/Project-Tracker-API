using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTracker_API.Models.DTOs.UserDTOs;
using ProjectTracker_API.Services.Abstract;
using System.Threading.Tasks;

namespace ProjectTracker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            var response = await _authService.LoginAsync(user);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO user)
        {
            var response = await _authService.RegisterAsync(user);
            return Ok(response);
        }
    }
}