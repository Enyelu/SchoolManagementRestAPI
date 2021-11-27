using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface IFacultyService
    {
        Task<Response<string>> AddFaculty(NameDto requestDto);
        Task<Response<string>> DeactivateFacultyAsync(NameDto requestDto);
        Task<Response<IEnumerable<FacultyDepartmentsResponseDto>>> ReadDepartmentsInFacultyAsync(NameDto requestDto);
        Task<Response<IEnumerable<FacultyLecturerResponseDto>>> ReadLecturersInFacultyAsync(NameDto requestDto);
        Task<Response<IEnumerable<FacultyLecturerResponseDto>>> ReadNonAcademinStaffInFacultyAsync(NameDto requestDto);
    }
}
