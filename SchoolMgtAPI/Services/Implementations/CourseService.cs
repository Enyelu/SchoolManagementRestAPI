using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.AppUnitOfWork;
using Utilities.GeneralResponse;

namespace Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<string>> AddCourseAsync(Course course)
        {
            await _unitOfWork.Course.AddAsync(course);
            await _unitOfWork.SaveChangesAsync();
            return Response<string>.Success(null, $"Successfully added {course.Name}");
        }
       public async Task<Response<Course>> GetCourseByIdOrCourseCodeAsync(string courseCode = null, string courseId = null)
       {
            var course = await _unitOfWork.Course.GetCourseByIdOrCourseCodeAsync(courseCode, courseId);
            if (course == null)
            {
                return Response<Course>.Success(course,"Success");
            }
            return Response<Course>.Fail("Course not found");
       }
       public async Task<Response<string>> DeactivateCourseAsync(string courseCode = null, string courseId = null)
       {
            var response = await _unitOfWork.Course.DeactivateCourseAsync(courseCode,courseId);
            if(response == false)
            {
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(null, $"Successfully updated deactivated");
            }
            return Response<string>.Fail($"Successfully updated deactivated");

        }
       
       public async Task<Response<string>> UpdateCourseAsync(Course course)
       {
            _unitOfWork.Course.Update(course);
            await _unitOfWork.SaveChangesAsync();
            return Response<string>.Success(null, $"Successfully updated {course.Name}");
       }
    }
}
