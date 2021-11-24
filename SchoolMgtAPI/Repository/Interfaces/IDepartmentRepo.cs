using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IDepartmentRepo : IGenericRepository<Department>
    {
        Task<Department> GetDepartmentAsync(string departmentName);
        Task<IEnumerable<Department>> GetAllDepartmentDetailsAsync();
        Task<IEnumerable<Department>> GetActiveDepartmentAsync();
    }
}