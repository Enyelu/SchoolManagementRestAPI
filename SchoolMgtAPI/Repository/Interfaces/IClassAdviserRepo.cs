using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IClassAdviserRepo : IGenericRepository<ClassAdviser>
    {
        Task<ClassAdviser> GetClassAdviserByEmailAsync(string classAdviserEmail);
        Task<ClassAdviser> GetClassAdviserByDepartmentAndLevelAsync(int level, string department);
    }
}
