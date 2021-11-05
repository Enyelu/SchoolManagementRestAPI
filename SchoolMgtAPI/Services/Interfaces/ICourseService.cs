using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
     public interface ICourseService
     {
        Task<Response<string>> AddCourse(CourseDto courseDto);
        Task<Response<CourseDto>> GetCourseByIdOrCourseCode(string courseCode = null, string courseId = null);
        Task<Response<string>> DeactivateCourse(string courseCode = null, string courseId = null);
        Task<Response<string>> UpdateCourse(CourseUpdateDto course);
        Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadCourseStudents(string courseCode = null, string courseId = null);
        Task<Response<IEnumerable<CourseLecturerResponseDto>>> ReadCourseLecturers(string courseCode = null, string courseId = null);
     }
}
