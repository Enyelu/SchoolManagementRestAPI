using Models;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IClassAdviserRepo : IGenericRepository<ClassAdviser>
    {
        Task<ClassAdviser> GetClassAdviserByEmailAsync(string classAdviserEmail);
        Task<ClassAdviser> GetClassAdviserByDepartmentAndLevelAsync(int level, string department);
    }
}
