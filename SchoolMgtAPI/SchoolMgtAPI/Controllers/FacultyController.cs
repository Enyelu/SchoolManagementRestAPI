using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;
using Utilities.Dtos;

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
        public async Task<IActionResult> AddFaculty(NameDto requestDto)
        {
            var response = await _facultyService.AddFaculty(requestDto);
            return StatusCode(response.StatusCode,response);
        }

        [HttpPatch("Deactivate")]
        public async Task<IActionResult> DeactivateFaculty(NameDto requestDto)
        {
            var response = await _facultyService.DeactivateFacultyAsync(requestDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("AllDepartments")]
        public async Task<IActionResult> ReadDepartmentsInFaculty([FromQuery]NameDto requestDto)
        {
            var response = await _facultyService.ReadDepartmentsInFacultyAsync(requestDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("AllLecturers")]
        public async Task<IActionResult> ReadLecturersInFaculty([FromQuery]NameDto requestDto)
        {
            var response = await _facultyService.ReadLecturersInFacultyAsync(requestDto);
            return StatusCode(response.StatusCode, response);
        }
    }
}