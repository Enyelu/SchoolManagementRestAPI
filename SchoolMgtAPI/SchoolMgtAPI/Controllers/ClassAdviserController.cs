using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace SchoolMgtAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class ClassAdviserController : ControllerBase
    {
        private readonly IClassAdviserService _classAdviserService;

        public ClassAdviserController(IClassAdviserService classAdviserService)
        {
            _classAdviserService = classAdviserService;
        }

        [HttpGet("ClassAdviser")]
        public async Task<IActionResult> ReadClassAdviser([FromQuery]ReadClassAdviserDto requestDto)
        {
            var response = await _classAdviserService.ReadClassAdviserAsync(requestDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("ClassAdviser")]
        public async Task<IActionResult> AssignClassAdviser([FromBody] AssignClassAdviserDto requestDto)
        {
            var response = await _classAdviserService.AssignClassAdviserAsync(requestDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("DeactivateClassAdviser")]
        public async Task<IActionResult> DeactivateClassAdviserAsync([FromQuery]ReadClassAdviserDto requestDto)
        {
            var response = await _classAdviserService.DeactivateClassAdviserAsync(requestDto);
            return StatusCode(response.StatusCode, response);
        }
    }
}