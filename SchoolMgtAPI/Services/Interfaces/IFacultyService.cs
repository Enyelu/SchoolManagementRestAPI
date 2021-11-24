using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface IFacultyService
    {
        Task<Response<string>> AddFaculty(string facultyName);
        Task<Response<string>> DeactivateFacultyAsync(string facultyName);
        Task<Response<IEnumerable<FacultyDepartmentsResponseDto>>> ReadDepartmentsInFacultyAsync(string facultyName);
        Task<Response<IEnumerable<FacultyLecturerResponseDto>>> ReadLecturersInFacultyAsync(string facultyName);
        Task<Response<IEnumerable<FacultyLecturerResponseDto>>> ReadNonAcademinStaffInFacultyAsync(string facultyName);
    }
}
