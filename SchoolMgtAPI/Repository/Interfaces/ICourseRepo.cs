using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICourseRepo : IGenericRepository<Course>
    {
        Task<Course> GetCourseByIdOrCourseCodeAsync(string courseCode = null, string courseId = null);
        Task<IEnumerable<Course>> CourseLecturers(string courseCode = null, string courseId = null);
        Task<IEnumerable<Course>> CourseStudents(string courseCode = null, string courseId = null);
    }
}
