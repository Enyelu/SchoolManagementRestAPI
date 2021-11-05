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
        Task<Response<NonAcademicStaffResponseDto>> ReadNonAcademicStaffAsync(string staffId);
        Task<Response<IEnumerable<NonAcademicStaffResponseDto>>> ReadAllNonAcademicStaffAsync();
        Task<Response<string>> DeactivateNonAcademicStaffAsync(string staffId);
        Task<Response<string>> UpdateNonAcademicStaffAsync(NonAcademicStaffDto staff, string staffId);
    }
}
