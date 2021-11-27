using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;

namespace SchoolMgtAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyService _facultyService;

        public FacultyController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        [HttpPost("Faculty")]
        public async Task<IActionResult> AddFaculty(string facultyName)
        {
            var response = await _facultyService.AddFaculty( facultyName);
            return StatusCode(response.StatusCode,response);
        }

        [HttpPatch("Deactivate")]
        public async Task<IActionResult> DeactivateFaculty(string facultyName)
        {
            var response = await _facultyService.DeactivateFacultyAsync(facultyName);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("AllDepartments")]
        public async Task<IActionResult> ReadDepartmentsInFaculty(string facultyName)
        {
            var response = await _facultyService.ReadDepartmentsInFacultyAsync(facultyName);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("AllLecturers")]
        public async Task<IActionResult> ReadLecturersInFaculty(string facultyName)
        {
            var response = await _facultyService.ReadLecturersInFacultyAsync(facultyName);
            return StatusCode(response.StatusCode, response);
        }
    }
}