using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Dtos;

namespace SchoolMgtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }



        [HttpPost("Admit")]
        public IActionResult AdmitStudent()
        {
            return Ok();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterStudentAsync(RegisterStudentDto registerStudent)
        {
            var response = await _studentService.RegisterStudentAsync(registerStudent);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("ConfirmStudentEmail")]
        public async Task<IActionResult> ConfirmStudentEmail(string email, string token)
        {
            var response = await _studentService.ConfirmStudentEmailAsync(email, token);
            return Ok(response);
        }

        [HttpGet("StudentsInALevel")]
        public async Task<IActionResult> ReadAllStudentsInALevel(int level)
        {
            var response = await _studentService.ReadAllStudentsInALevelAsync(level);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("StudentsInADepartmentInALevel")]
        public async Task<IActionResult> ReadAllStudentsInADepartmentInALevel(int level, string department)
        {
            var response = await _studentService.ReadAllStudentsInADepartmentInALevelAsync(level, department);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("StudentsInAFacultyInALevel")]
        public async Task<IActionResult> ReadAllStudentsInAFacultyInALevel(int level, string faculty)
        {
            var response = await _studentService.ReadAllStudentsInAFacultyInALevelAsync(level, faculty);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("StudentsInADepartment")]
        public async Task<IActionResult> ReadAllStudentsInDepartment(string department)
        {
            var response = await _studentService.ReadAllStudentsInDepartmentAsync(department);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("StudentsInAFaculty")]
        public async Task<IActionResult> ReadAllStudentsInFaculty(string faculty)
        {
            var response = await _studentService.ReadAllStudentsInFacultyAsync(faculty);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("AllStudents")]
        public async Task<IActionResult> ReadAllStudent()
        {
            var response = await _studentService.ReadAllStudentAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("DeactivateStudent")]
        public async Task<IActionResult> DeactivateStudent(string registrationNumber)
        {
            var response = await _studentService.DeactivateStudentAsync(registrationNumber);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("StudentIsActive")]
        public async Task<IActionResult> CheckStudentIsActive(string registrationNumber)
        {
            var response = await _studentService.CheckStudentIsActiveAsync(registrationNumber);
            return StatusCode(response.StatusCode, response);
        }

        //[Authorize(Roles = Roles.Student)]
        [HttpPost("RegisterCourse")]
        public async Task<IActionResult> RegisterCourses(string studentId, ICollection<string> courses)
        {
            var response = await _studentService.RegisterCoursesAsync(studentId, courses);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("RemoveCourses")]
        public async Task<IActionResult> RemoveCourses(string studentId, ICollection<string> courses)
        {
            var response = await _studentService.RemoveCoursesAsync(studentId, courses);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("RegisteredCourses")]
        public async Task<IActionResult> ReadRegisteredCourses(string studentId)
        {
            var response = await _studentService.ReadRegisteredCoursesAsync(studentId);
            return StatusCode(response.StatusCode, response);
        }
    }
}

