using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IDepartmentRepo : IGenericRepository<Department>
    {
        Task<Department> GetDepartmentAsync(string departmentName);
        Task<IEnumerable<Department>> GetAllDepartmentDetailsAsync();
        Task<IEnumerable<Department>> GetAllDepartmentAsync();
    }
}