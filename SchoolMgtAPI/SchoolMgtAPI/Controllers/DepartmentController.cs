using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Services.Interfaces;
using System.IO;
using System.Threading.Tasks;
using Utilities.AppUnitOfWork;

namespace SchoolMgtAPI.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly UserManager<AppUser> _userManager;

        public DepartmentController(IDepartmentService departmentService, UserManager<AppUser> userManager)
        {
            _departmentService = departmentService;
            _userManager = userManager;

        }
        
        [HttpPost("Department")]
        public async Task<IActionResult> AddDepartment(string departmentName, string facultyName)
        {
           var response  =  await _departmentService.AddDepartmentAsync(departmentName, facultyName);
           return StatusCode(response.StatusCode,response);
        }

        [HttpPatch("Deactivate")]
        public async Task<IActionResult> DeactivateDepartment(string departmentName)
        {
            var response = await _departmentService.DeactivateDepartmentAsync(departmentName);

            if(response)
            {
                return StatusCode(200, response);
            }
            return BadRequest();
        }

        [HttpGet("AllDepartments")]
        public async Task<IActionResult> ReadAllDepartments()
        {
            var response = await _departmentService.ReadAllDepartmentsAsync();
            return StatusCode(response.StatusCode,response);
        }

        [HttpPatch("LecturerToDepartment")]
        public async Task<IActionResult> AddLecturerToDepartment(string lecturerEmail, string departmentName)
        {
            var response = await _departmentService.AddLecturerToDepartmentAsync(lecturerEmail, departmentName);
            return StatusCode(response.StatusCode,response);
        }

        [HttpPatch("DeactivateLecturerFromDepartment")]
        public async Task<IActionResult> DeactivateLecturerFromDepartment(string lecturerEmail)
        {
            var response = await _departmentService.DeactivateLecturerFromDepartmentAsync(lecturerEmail);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("AllLecturersInADepartment")]
        public async Task<IActionResult> GetAllLecturersInADepartment(string departmentName)
        {
            var response = await _departmentService.GetAllLecturersInADepartmentAsync(departmentName);
            return StatusCode(response.StatusCode, response);
        }

        //[HttpPost("CourseToDepartment")]
        //public async Task<IActionResult> AddCourseToDepartment(string departmentName, string courseCode)
        //{
        //    var response = await _departmentService.AddCourseToDepartmentAsync(departmentName, courseCode);
        //    return StatusCode(response.StatusCode, response);
        //}

        //[HttpPatch("DeactivateDepartmentCourse")]
        //public async Task<IActionResult> DeactivateDepartmentCourse(string departmentName, string courseCode)
        //{
        //    var response = await _departmentService.DeactivateDepartmentCourseAsycn(departmentName, courseCode);
        //    return StatusCode(response.StatusCode, response);
        //}

        [HttpGet("GetDeparmentCourses")]
        public async Task<IActionResult> GetDeparmentCourses(string departmentName)
        {
            var response = await _departmentService.GetDeparmentCoursesAsync(departmentName);
            return StatusCode(response.StatusCode, response);
        }
    }
}