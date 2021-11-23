using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;

namespace SchoolMgtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassAdviserController : ControllerBase
    {
        private readonly IClassAdviserService _classAdviserService;

        public ClassAdviserController(IClassAdviserService classAdviserService)
        {
            _classAdviserService = classAdviserService;
        }

        ////[HttpGet("ClassAdviser")]
        //public async Task<IActionResult> ReadClassAdviser(int level, string department)
        //{
        //    var response = await _classAdviserService.ReadClassAdviser(level, department);
        //    return StatusCode(response.StatusCode, response);
        //}

        ////[HttpPost("ClassAdviser")]
        //public async Task<IActionResult> AssignClassAdviser(string lecturerEmail, int level)
        //{
        //    var response = await _classAdviserService.AssignClassAdviser(lecturerEmail, level);
        //    return StatusCode(response.StatusCode, response);
        //}
    }
}