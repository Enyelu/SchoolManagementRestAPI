using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface ICourseService
    {
        Task<Response<Course>> GetCourseByIdOrCourseCodeAsync(string courseCode = null, string courseId = null);
        Task<Response<string>> DeactivateCourseAsync(string courseCode = null, string courseId = null);
        Task<Response<string>> UpdateCourseAsync(Course course);
        Task<Response<string>> AddCourseAsync(Course course);
    }
}
