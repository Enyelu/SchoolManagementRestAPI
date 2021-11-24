using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface INonAcademicStaffService
    {
        Task<Response<string>> RegisterNonAcademicStaff(RegisterNonAcademicStaffDto NonAcademicStaffDto);
        Task<Response<NonAcademicStaffResponseDto>> ReadNonAcademicStaffAsync(string staffEmail);
        Task<Response<IEnumerable<NonAcademicStaffResponseDto>>> ReadAllNonAcademicStaffAsync();
        Task<Response<string>> DeactivateNonAcademicStaffAsync(string staffEmail);
        Task<Response<string>> UpdateNonAcademicStaffAsync(NonAcademicStaffUpdateDto staff, string staffEmail);
    }
}
