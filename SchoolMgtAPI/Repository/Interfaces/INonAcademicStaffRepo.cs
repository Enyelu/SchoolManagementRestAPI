using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface INonAcademicStaffRepo
    {
        Task<NonAcademicStaff> GetNonAcademicStaffAsync(string staffId);
        Task<IEnumerable<NonAcademicStaff>> GetAllNonAcademicStaffAsync();
        Task<bool> DeactivateNonAcademicStaffAsync(string staffId);
        bool UpdateNonAcademicStaff(NonAcademicStaff staff);
    }
}
