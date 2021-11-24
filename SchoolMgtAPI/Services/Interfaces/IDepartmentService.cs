using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<Response<string>> AddDepartmentAsync(string departmentName, string facultyName);
        Task<bool> DeactivateDepartmentAsync(string departmentName);
        Task<Response<IEnumerable<ReadDepartmentDto>>> ReadAllDepartmentsAsync();
        Task<Response<string>> AddLecturerToDepartmentAsync(string lecturerEmail, string departmentName);
        Task<Response<string>> DeactivateLecturerFromDepartmentAsync(string lecturerEmail);
        Task<Response<IEnumerable<LecturerResponseDto>>> GetAllLecturersInADepartmentAsync(string departmentName);
        Task<Response<IEnumerable<CourseDto>>> GetDeparmentCoursesAsync(string departmentName);
    }
}
