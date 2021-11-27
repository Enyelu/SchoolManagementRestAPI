using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace SchoolMgtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost()]
        public async Task<IActionResult> AddDepartment(DepartmentRequestDto requestDto)
        {
            var response = await _departmentService.AddDepartmentAsync(requestDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("Deactivate")]
        public async Task<IActionResult> DeactivateDepartment(NameDto departmentName)
        {
            var response = await _departmentService.DeactivateDepartmentAsync(departmentName);

            if (response)
            {
                return StatusCode(200, response);
            }
            return StatusCode(200, response);
        }
         
        [HttpGet("AllDepartments")]
        public async Task<IActionResult> ReadAllDepartments()
        {
            var response = await _departmentService.ReadAllDepartmentsAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost("Lecturer")]
        public async Task<IActionResult> AddLecturerToDepartment(AddLecturerRequestDto requestDto)
        {
            var response = await _departmentService.AddLecturerToDepartmentAsync(requestDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("DeactivateLecturer")]
        public async Task<IActionResult> DeactivateLecturerFromDepartment(EmailRequestDto lecturerEmail)
        {
            var response = await _departmentService.DeactivateLecturerFromDepartmentAsync(lecturerEmail);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("AllLecturers")]
        public async Task<IActionResult> GetAllLecturersInADepartment(NameDto departmentName)
        {
            var response = await _departmentService.GetAllLecturersInADepartmentAsync(departmentName);
            return StatusCode(response.StatusCode, response);
        }


        [HttpGet("Courses")]
        public async Task<IActionResult> GetDeparmentCourses(NameDto departmentName)
        {
            var response = await _departmentService.GetDeparmentCoursesAsync(departmentName);
            return StatusCode(response.StatusCode, response);
        }
    }
}
