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

        public async Task<Course> GetCourseByIdOrCourseCodeAsync(string courseCode = null, string courseId = null)
        {
            string searchTerm = courseCode ?? courseId;
            var courseById = await _context.Courses
                                           .Include(x => x.Students)
                                           .Include(x => x.Department)
                                           .Include(x => x.Lecturers)
                                           .FirstOrDefaultAsync(x => x.Id == searchTerm);
            var courseByCode = await _context.Courses
                                           .Include(x => x.Students)
                                           .Include(x => x.Department)
                                           .Include(x => x.Lecturers)
                                           .FirstOrDefaultAsync(x => x.Id == searchTerm);

            if (courseById == null)
            {
                return courseByCode;
            }
            else if(courseByCode == null)
            {
                return courseById;
            }
            return null;
        }

        public async Task<bool> DeactivateCourseAsync(string courseCode = null, string courseId = null)
        {
            var course = await GetCourseByIdOrCourseCodeAsync(courseCode, courseId);
            if(course.IsActive == true)
            {
                course.IsActive = false;
            }
            return course.IsActive;
        }
    }
}
