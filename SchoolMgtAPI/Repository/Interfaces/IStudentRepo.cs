using Models;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IStudentRepo : IGenericRepository<Student>
    {
        Task<Student> GetStudentAsync(string studentId);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task DeactivateStudentAsync(string studentId);
        Task<bool> CheckStudentIsActiveAsync(string studentId);
        Task<IEnumerable<Course>> RegisterCoursesAsync(string studentId, ICollection<Course> courses);
        Task<bool> RemoveCoursesAsync(string studentId, ICollection<Course> courses);
        Task<IEnumerable<Course>> GetRegisteredCoursesAsync(string studentId);
    }
}
