using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("StudentsInALevel")]
        public async Task<IActionResult> ReadAllStudentsInALevel(LevelDto requestDto)
        {
            var response = await _studentService.ReadAllStudentsInALevelAsync(requestDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("StudentsInADepartmentInALevel")]
        public async Task<IActionResult> ReadAllStudentsInADepartmentInALevel(ReadDepartmentsStudentDto studentDto)
        {
            var response = await _studentService.ReadAllStudentsInADepartmentInALevelAsync(studentDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("StudentsInAFacultyInALevel")]
        public async Task<IActionResult> ReadAllStudentsInAFacultyInALevel(ReadFacultyStudentsDto studentsDto)
        {
            var response = await _studentService.ReadAllStudentsInAFacultyInALevelAsync(studentsDto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("StudentsInADepartment")]
        public async Task<IActionResult> ReadAllStudentsInDepartment(NameDto department)
        {
            var response = await _studentService.ReadAllStudentsInDepartmentAsync(department);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("StudentsInAFaculty")]
        public async Task<IActionResult> ReadAllStudentsInFaculty(NameDto faculty)
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
        public async Task<IActionResult> DeactivateStudent(RegistrationNumberDto registrationNumber)
        {
            var response = await _studentService.DeactivateStudentAsync(registrationNumber);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("StudentIsActive")]
        public async Task<IActionResult> CheckStudentIsActive(RegistrationNumberDto registrationNumber)
        {
            var response = await _studentService.CheckStudentIsActiveAsync(registrationNumber);
            return StatusCode(response.StatusCode, response);
        }

       // [Authorize(Roles = Roles.Student)]
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

