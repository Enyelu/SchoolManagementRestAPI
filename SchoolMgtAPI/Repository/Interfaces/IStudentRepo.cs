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
    Task<Student> GetStudentAsync(string registrationNumber = null, string studentId = null);
    Task<IEnumerable<Student>> GetAllStudentsInALevelAsync(int studentsLevel);
    Task<IEnumerable<Student>> GetAllStudentsInADepartmentInALevelAsync(int studentsLevel, string department);
    Task<IEnumerable<Student>> GetAllStudentsInAFacultyInALevelAsync(int studentsLevel, string faculty);
    Task<IEnumerable<Student>> GetAllStudentsInDepartmentAsync(string department);
    Task<IEnumerable<Student>> GetAllStudentsInFacultyAsync(string faculty);
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task<IEnumerable<string>> GetRegisteredCoursesAsync(string studentId);
    }
}

