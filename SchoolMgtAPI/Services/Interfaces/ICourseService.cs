using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
     public interface ICourseService
     {
        Task<Response<string>> AddCourseAsync(CourseDto courseDto);
        Task<Response<CourseDto>> GetCourseByIdOrCourseCodeAsync(string courseCode = null, string courseId = null);
        Task<Response<string>> DeactivateCourseAsync(string courseCode = null, string courseId = null);
        Task<Response<string>> UpdateCourseAsync(CourseUpdateDto course);
        Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadCourseStudentsAsync(string courseCode = null, string courseId = null);
        Task<Response<IEnumerable<CourseLecturerResponseDto>>> ReadCourseLecturersAsync(string courseCode = null, string courseId = null);
     }
}
