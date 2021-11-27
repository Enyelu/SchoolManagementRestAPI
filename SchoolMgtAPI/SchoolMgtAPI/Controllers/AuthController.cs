using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;

namespace SchoolMgtAPI.Controllers
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

        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var response = await _authService.ConfirmEmailAsync(email, token);
            return StatusCode(200, response);
        }

        //[HttpPost("Login")]
        //public async Task<IActionResult> Login()
        //{

        //}
    }
}
