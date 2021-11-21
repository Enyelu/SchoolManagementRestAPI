using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace SchoolMgtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturerController : ControllerBase
    {
        private readonly ILecturerService _lecturerService;

        public LecturerController(ILecturerService lecturerService)
        {
            _lecturerService = lecturerService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterLecturer(RegisterLecturerDto lecturerDto)
        {
            var respose = await _lecturerService.RegisterLecturerAsync(lecturerDto);
            return StatusCode(respose.StatusCode, respose);
        }

        [HttpGet("Lecturer")]
        public async Task<IActionResult> ReadLecturerDetail(string lecturerEmail)
        {
            var respose = await _lecturerService.ReadLecturerDetailAsync(lecturerEmail);
            return StatusCode(respose.StatusCode, respose);
        }

        [HttpPatch("Deactivate")]
        public async Task<IActionResult> DeactivateLecturer(string lecturerEmail)
        {
            var respose = await _lecturerService.DeactivateLecturerAsync(lecturerEmail);
            return StatusCode(respose.StatusCode, respose);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateLecturerAsync(LecturerDto lecturerDto, string lecturerEmail)
        {
            var respose = await _lecturerService.UpdateLecturerAsync(lecturerDto, lecturerEmail);
            return StatusCode(respose.StatusCode, respose);
        }

        [HttpGet("AssignCourses")]
        public async Task<IActionResult> AssignCourseToLecturer(string lecturerEmail, string courseName, string courseCode)
        {
            var response = await _lecturerService.AssignCourseToLecturerAsync(lecturerEmail, courseName, courseCode);
            return StatusCode(response.StatusCode, response);
        }
    }
}