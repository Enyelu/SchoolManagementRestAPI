using Models;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICourseRepo : IGenericRepository<Course>
    {
        Task<Course> GetCourseByNameOrCourseCodeAsync(string courseCode = null, string courseName = null);
    }
}
