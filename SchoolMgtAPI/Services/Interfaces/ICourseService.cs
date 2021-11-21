using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{ 
     public interface ICourseService
     {
        Task<Response<string>> AddCourseAsync(CourseDto courseDto);
        Task<Response<CourseDto>> GetCourseByNameOrCourseCodeAsync(string courseCode = null, string courseId = null);
        Task<Response<string>> DeactivateCourseAsync(string courseCode = null, string courseName = null);
        Task<Response<string>> UpdateCourseAsync(CourseUpdateDto course, string CourseCode);
        Task<Response<IEnumerable<StudentModel>>> ReadCourseStudentsAsync(string courseCode = null, string courseName = null);
        Task<Response<IEnumerable<LecturerModel>>> ReadCourseLecturersAsync(string courseCode = null, string courseName = null);
     }
}
