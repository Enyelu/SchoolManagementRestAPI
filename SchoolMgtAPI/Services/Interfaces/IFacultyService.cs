using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface IFacultyService
    {
        Task<Response<string>> AddFaculty(string facultyName);
        Task<Response<string>> DeactivateFaculty(string facultyName);
        Task<Response<IEnumerable<FacultyDepartmentsResponseDto>>> ReadDepartmentsInFaculty(string facultyName);
        Task<Response<IEnumerable<FacultyLecturerResponseDto>>> ReadLecturersInFaculty(string facultyName);
        Task<Response<IEnumerable<FacultyLecturerResponseDto>>> ReadNonAcademinStaffInFaculty(string facultyName);
    }
}
