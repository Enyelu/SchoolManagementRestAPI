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
                                           .Include(x => x.Lecturers)
                                           .FirstOrDefaultAsync(x => x.Name == searchTerm || x.CourseCode == searchTerm);
            if (course != null) { return course; }
            
            return null;
        }
    }
}