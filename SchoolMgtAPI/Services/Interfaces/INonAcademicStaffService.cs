using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        Task<Response<string>> UpdateNonAcademicStaffAsync(NonAcademicStaffDto staff, string staffEmail);
    }
}
