using Models;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ILecturerRepo :IGenericRepository<Lecturer>
    {
        Task<Lecturer> GetLecturerDetailAsync(string lecturerEmail);
    }
}
