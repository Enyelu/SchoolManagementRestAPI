using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<Response<string>> AddDepartmentAsync(DepartmentRequestDto requestDto);
        Task<bool> DeactivateDepartmentAsync(NameDto departmentName);
        Task<Response<IEnumerable<ReadDepartmentDto>>> ReadAllDepartmentsAsync();
        Task<Response<string>> AddLecturerToDepartmentAsync(AddLecturerRequestDto requestDto);
        Task<Response<string>> DeactivateLecturerFromDepartmentAsync(EmailRequestDto lecturerEmail);
        Task<Response<IEnumerable<LecturerResponseDto>>> GetAllLecturersInADepartmentAsync(NameDto departmentName);
        Task<Response<IEnumerable<CourseDto>>> GetDeparmentCoursesAsync(NameDto departmentName);
    }
}
