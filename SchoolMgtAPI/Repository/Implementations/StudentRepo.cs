using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class StudentRepo : GenericRepository<Student>, IStudentRepo
    {
        private readonly SchoolDbContext _context;
        private readonly DbSet<Student> studentTable;
        public StudentRepo(SchoolDbContext context) : base(context)
        {
            _context = context;
            studentTable = _context.Set<Student>();
        }

        public async Task<Student> GetStudentAsync(string registrationNumber)
        {
            if (registrationNumber != null)
            {
                var student = await studentTable
                          .Include(x => x.AppUser)
                          .Include(x => x.ClassAdviser)
                          .Include(x => x.Department)
                          .Include(x => x.Courses)
                          .FirstOrDefaultAsync(x => x.RegistrationNumber == registrationNumber);

                if (student != null) { return student; }
            }
            return null;
        }
        public async Task<IEnumerable<Student>> GetAllStudentsInALevelAsync(int studentsLevel)
        {
            var students = studentTable
                          .Include(x => x.AppUser)
                          .Include(x => x.Department)
                          .Include(x => x.Faculty)
                          .Where(x => x.Level == studentsLevel);

            if (students != null)
            {
                return await students.ToListAsync();
            }
            return null;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsInADepartmentInALevelAsync(int studentsLevel, string department)
        {
            var students = studentTable
                          .Include(x => x.AppUser)
                          .Include(x => x.Department)
                          .Where(x => x.Level == studentsLevel && x.Department.Name.Trim().ToLower() == department.Trim().ToLower());

            if (students != null)
            {
                return await students.ToListAsync();
            }
            return null;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsInAFacultyInALevelAsync(int studentsLevel, string faculty)
        {
            var students = studentTable
                          .Include(x => x.AppUser)
                          .Include(x => x.Department)
                          .Include(x => x.Faculty)
                          .Where(x => x.Level == studentsLevel && x.Faculty.Name.Trim().ToLower() == faculty.Trim().ToLower());

            if (students != null)
            {
                return await students.ToListAsync();
            }
            return null;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsInDepartmentAsync(string department)
        {
            var students = studentTable
                          .Include(x => x.AppUser)
                          .Include(x => x.Department)
                          .Include(x => x.Faculty)
                          .Where(x => x.Department.Name.Trim().ToLower() == department.Trim().ToLower());

            if (students != null)
            {
                return await students.ToListAsync();
            }
            return null;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsInFacultyAsync(string faculty)
        {
            var students = studentTable
                          .Include(x => x.AppUser)
                          .Include(x => x.Faculty)
                          .Where(x => x.Faculty.Name.Trim().ToLower() == faculty.Trim().ToLower());

            if (students != null)
            {
                return await students.ToListAsync();
            }
            return null;
        }


        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            var students = studentTable
                          .Include(x => x.AppUser)
                          .Include(x => x.ClassAdviser)
                          .Include(x => x.Department)
                          .Include(x => x.Courses).Where(x => x.AppUser.IsActive == true);
            
            if(students != null)
            {
                return await students.ToListAsync();
            }
            return null;
        }

        public async Task DeactivateStudentAsync(string studentId)
        {
            var student = await GetStudentAsync(studentId);
            student.AppUser.IsActive = false;
        }

        public async Task<bool> CheckStudentIsActiveAsync(string registrationNumber = null)
        {
            var student = await GetStudentAsync(registrationNumber);
            var respone = student.AppUser.IsActive;
            
            return respone;
        }

        public async Task<IEnumerable<string>> RegisterCoursesAsync(string studentId, ICollection<string> courses)
        {
            var student = await GetStudentAsync(studentId);
            var courseCount = student.Courses.Count;

            if (student != null)
            {
                foreach (var course in courses)
                {
                    student.Courses.Add(course);
                }
                
                if(student.Courses.Count.Equals(courses.Count) || (student.Courses.Count - courseCount).Equals(courses.Count))
                {
                    return student.Courses.ToList();
                }
            }
            return null;
        }

        public async Task<bool> RemoveCoursesAsync(string studentId, ICollection<string> courses)
        {
            var student = await GetStudentAsync(studentId);
            var courseCount = student.Courses.Count;
            var registeredCourses = student.Courses;

            foreach (var course in registeredCourses)
            {
                courses.Contains(course);
                registeredCourses.Remove(course);
            }

            if(courseCount - student.Courses.Count == courses.Count)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<string>> GetRegisteredCoursesAsync(string studentId)
        {
           var student = await GetStudentAsync(studentId);
           var courses = student.Courses.ToList();

            if(courses != null)
            {
                return courses;
            }
            return null;
        }
    }
}
