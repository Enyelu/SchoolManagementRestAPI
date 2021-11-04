using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface ICourseService
    {
        Task<Response<Course>> GetCourseByIdOrCourseCodeAsync(string courseCode = null, string courseId = null);
        Task<Response<string>> DeactivateCourseAsync(string courseCode = null, string courseId = null);
        Task<Response<string>> UpdateCourseAsync(CourseUpdateDto course);
        Task<Response<string>> AddCourseAsync(Course course);
        Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadCourseStudents(string courseCode = null, string courseId = null);
        Task<Response<IEnumerable<CourseLecturerResponseDto>>> ReadCourseLecturers(string courseCode = null, string courseId = null);
    }
}
