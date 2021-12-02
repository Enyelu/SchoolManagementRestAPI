using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace SchoolMgtAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost("Course")]
        public async Task<IActionResult> RegisterAsync([FromBody] CourseDto courseDto)
        {
            var response = await _courseService.AddCourseAsync(courseDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("Course")]
        public async Task<IActionResult> GetCourseByIdOrCourseCodeAsync(string courseCode = null, string courseName = null)
        {
            var response = await _courseService.GetCourseByNameOrCourseCodeAsync(courseCode, courseName);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("Deactivate")]
        public async Task<IActionResult> DeactivateCourseAsync(string courseCode = null, string courseName = null)
        {
            var response = await _courseService.DeactivateCourseAsync(courseCode, courseName);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCourse([FromQuery]CourseUpdateDto courseUpdateDto, string courseCode)
        {
            var response = await _courseService.UpdateCourseAsync(courseUpdateDto, courseCode);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("CourseStudents")]
        public async Task<IActionResult> ReadCourseStudents(string courseCode = null, string courseName = null)
        {
            var response = await _courseService.ReadCourseStudentsAsync(courseCode, courseName);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("CourseLecturers")]
        public async Task<IActionResult> ReadCourseLecturers(string courseCode = null, string courseName = null)
        {
            var response = await _courseService.ReadCourseLecturersAsync(courseCode, courseName);
            return StatusCode(response.StatusCode, response);
        }
    }
}