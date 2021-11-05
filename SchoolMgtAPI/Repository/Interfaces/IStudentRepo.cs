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
    Task<Student> GetStudent(string registrationNumber = null, string studentId = null);
    Task<IEnumerable<Student>> GetAllStudentsInALevel(int studentsLevel);
    Task<IEnumerable<Student>> GetAllStudentsInADepartmentInALevel(int studentsLevel, string department);
    Task<IEnumerable<Student>> GetAllStudentsInAFacultyInALevel(int studentsLevel, string faculty);
    Task<IEnumerable<Student>> GetAllStudentsInDepartment(string department);
    Task<IEnumerable<Student>> GetAllStudentsInFaculty(string faculty);
    Task<IEnumerable<Student>> GetAllStudents();
    Task<IEnumerable<string>> GetRegisteredCourses(string studentId);
    }
}

