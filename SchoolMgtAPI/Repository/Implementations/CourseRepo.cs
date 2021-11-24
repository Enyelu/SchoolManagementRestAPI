using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository.Interfaces;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class CourseRepo : GenericRepository<Course>, ICourseRepo
    {
        private readonly SchoolDbContext _context;

        public CourseRepo(SchoolDbContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<Course> GetCourseByNameOrCourseCodeAsync(string courseCode = null, string courseName = null)
        {
            string searchTerm = courseCode ?? courseName;
            var course = await _context.Courses
                                           .Include(x => x.Students)
                                           .Include(x => x.Department)
                                           .Include(x => x.Faculty)
                                           .Include(x => x.Lecturers)
                                           .ThenInclude(x => x.AppUser)
                                           .ThenInclude(x => x.Address)
                                           .FirstOrDefaultAsync(x => x.Name.Trim().ToLower() == searchTerm.Trim().ToLower()
                                                           || x.CourseCode.Trim().ToLower() == searchTerm.Trim().ToLower());
            if (course != null) { return course; }
            
            return null;
        }
    }
}