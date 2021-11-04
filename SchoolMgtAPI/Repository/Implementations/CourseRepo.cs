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
            var course = await _context.Courses
                                           .Include(x => x.Students)
                                           .Include(x => x.Department)
                                           .Include(x => x.Lecturers)
                                           .FirstOrDefaultAsync(x => x.Id == searchTerm || x.CourseCode == searchTerm);
            if (course != null)
            {
                return course;
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

        public async Task<IEnumerable<Course>> CourseLecturers(string courseCode = null, string courseId = null)
        {
            string searchTerm = courseCode ??= courseId;
            var courseWithLecturers = _context.Courses.Include(x => x.Lecturers).Where(x => x.CourseCode == courseCode || x.Id == courseId);

            if (courseWithLecturers != null) { return await courseWithLecturers.ToListAsync(); }

            return null;
        }

        public async Task<IEnumerable<Course>> CourseStudents(string courseCode = null, string courseId = null)
        {
            string searchTerm = courseCode ??= courseId;
            var courseWithLecturers = _context.Courses.Include(x => x.Students).Where(x => x.CourseCode == courseCode || x.Id == courseId);

            if (courseWithLecturers != null) { return await courseWithLecturers.ToListAsync(); }

            return null;
        }
    }
}
