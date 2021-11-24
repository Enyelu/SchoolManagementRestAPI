using Models;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IFacultyRepo : IGenericRepository<Faculty>
    {
        Task<Faculty> GetFacultyAsync(string facultyName);
    }
}
