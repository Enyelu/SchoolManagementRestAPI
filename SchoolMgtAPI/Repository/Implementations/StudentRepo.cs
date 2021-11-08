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

        public async Task<Student> GetStudentAsync(string registrationNumber = null, string studentId = null)
        {
            string searchTerm = registrationNumber ??= studentId;
            if (searchTerm != null)
            {
                var student = await studentTable
                          .Include(x => x.AppUser)
                          .Include(x => x.ClassAdviser)
                          .Include(x => x.Department)
                          .Include(x => x.Courses)
                          .FirstOrDefaultAsync(x => x.RegistrationNumber == searchTerm 
                                                 || x.AppUser.Id == searchTerm);

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

            if (students != null) { return await students.ToListAsync(); }
           
            return null;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsInADepartmentInALevelAsync(int studentsLevel, string department)
        {
            var students = studentTable
                          .Include(x => x.AppUser)
                          .Include(x => x.Department)
                          .Where(x => x.Level == studentsLevel && x.Department.Name.Trim().ToLower() == department.Trim().ToLower());

            if (students != null) { return await students.ToListAsync(); }
            
            return null;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsInAFacultyInALevelAsync(int studentsLevel, string faculty)
        {
            var students = studentTable
                          .Include(x => x.AppUser)
                          .Include(x => x.Department)
                          .Include(x => x.Faculty)
                          .Where(x => x.Level == studentsLevel && x.Faculty.Name.Trim().ToLower() == faculty.Trim().ToLower());

            if (students != null) { return await students.ToListAsync(); }
           
            return null;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsInDepartmentAsync(string department)
        {
            var students = studentTable
                          .Include(x => x.AppUser)
                          .Include(x => x.Department)
                          .Include(x => x.Faculty)
                          .Where(x => x.Department.Name.Trim().ToLower() == department.Trim().ToLower());

            if (students != null) { return await students.ToListAsync(); }
            
            return null;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsInFacultyAsync(string faculty)
        {
            var students = studentTable
                          .Include(x => x.AppUser)
                          .Include(x => x.Faculty)
                          .Where(x => x.Faculty.Name.Trim().ToLower() == faculty.Trim().ToLower());

            if (students != null) { return await students.ToListAsync(); }
            
            return null;
        }


        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            var students = studentTable
                          .Include(x => x.AppUser)
                          .Include(x => x.ClassAdviser)
                          .Include(x => x.Department)
                          .Include(x => x.Courses).Where(x => x.AppUser.IsActive == true);
            
            if(students != null) { return await students.ToListAsync(); }
            return null;
        }
        public async Task<IEnumerable<Course>> GetRegisteredCoursesAsync(string studentId)
        {
           var student = await GetStudentAsync(null, studentId);
           var courses = student.Courses.ToList();

            if(courses != null) { return courses; }
           
            return null;
        }
    }
}
