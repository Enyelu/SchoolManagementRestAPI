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
        Task<Student> GetStudentAsync(string registrationNumber);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task DeactivateStudentAsync(string studentId);
        Task<bool> CheckStudentIsActiveAsync(string studentId);
        Task<IEnumerable<string>> RegisterCoursesAsync(string studentId, ICollection<string> courses);
        Task<bool> RemoveCoursesAsync(string studentId, ICollection<string> courses);
        Task<IEnumerable<string>> GetRegisteredCoursesAsync(string studentId);
        Task<IEnumerable<Student>> GetAllStudentsInALevelAsync(int studentsLevel);
        Task<IEnumerable<Student>> GetAllStudentsInDepartmentAsync(string department);
        Task<IEnumerable<Student>> GetAllStudentsInFacultyAsync(string faculty);
        Task<IEnumerable<Student>> GetAllStudentsInADepartmentInALevelAsync(int studentsLevel, string department);
        Task<IEnumerable<Student>> GetAllStudentsInAFacultyInALevelAsync(int studentsLevel, string faculty);
    }
}
