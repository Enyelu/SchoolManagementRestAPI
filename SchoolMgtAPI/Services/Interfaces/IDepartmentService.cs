using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface IDepartmentService
    {
        Task AddDepartmentAsync(string departmentName, string facultyName);
        Task<bool> DeactivateDepartmentAsync(string departmentName);
        Task<Response<IEnumerable<Department>>> ReadAllDepartments();
        Task<Response<string>> AddLecturerToDepartment(string lecturerEmail, string departmentName);
        Task<Response<string>> DeactivateLecturerFromDepartmentAsync(string lecturerEmail);
        Task<Response<IEnumerable<Lecturer>>> GetAllLecturersInADepartmentAsync(string departmentName);
        Task<Response<string>> AddCourseToDepartmentAsync(string departmentName, string courseCode);
        Task<Response<string>> DeactivateDepartmentCourseAsycn(string departmentName, string courseCode);
        Task<Response<IEnumerable<Course>>> GetDeparmentCoursesAsync(string departmentName);
    }
}
