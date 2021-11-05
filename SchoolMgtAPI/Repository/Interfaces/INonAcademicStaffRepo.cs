using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface INonAcademicStaffRepo : IGenericRepository <NonAcademicStaff>
    { 
        Task<NonAcademicStaff> GetNonAcademicStaffAsync(string staffId);
        Task<IEnumerable<NonAcademicStaff>> GetAllNonAcademicStaffAsync();
    }
}
