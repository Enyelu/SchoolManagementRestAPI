using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities.AppUnitOfWork;
using Utilities.GeneralResponse;

namespace Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Response<Student>> ReadStudentAsync(string studentId)
        {
            var response = await _unitOfWork.Student.GetStudentAsync(studentId);

            if(response != null)
            {
                return Response<Student>.Success(response, "Student Found");
            }
            return Response<Student>.Fail("Student not found");
        }

        public async Task<Response<IEnumerable<Student>>> ReadAllStudentAsync()
        {
            var response =  await _unitOfWork.Student.GetAllStudentsAsync();

            if(response != null)
            {
                return Response<IEnumerable<Student>>.Success(response, "Success");
            }
            return Response<IEnumerable<Student>>.Success(response, "Failed to fetch data");
        }

        public async Task<Response<string>> DeactivateStudentAsync(string studentId)
        {
            await _unitOfWork.Student.DeactivateStudentAsync(studentId);
            await _unitOfWork.SaveChangesAsync();
            return Response<string>.Success(null, "Student deactivated successfully");
        }
        public async Task<Response<string>> CheckStudentIsActiveAsync(string studentId)
        {
            var response = await _unitOfWork.Student.CheckStudentIsActiveAsync(studentId);

            if(response == true)
            {
                return Response<string>.Success(null, "Student is active");
            }
            return Response<string>.Fail("Student is not active");
        }

        public async Task<Response<IEnumerable<Course>>> RegisterCoursesAsync(string studentId, ICollection<Course> courses)
        {
            var response = await _unitOfWork.Student.RegisterCoursesAsync(studentId, courses);
            
            if(response != null)
            {
                await _unitOfWork.SaveChangesAsync();
                return Response< IEnumerable < Course >>.Success(response,"Registration successful");
            }
            string failMessage = "An error occured while adding courses. Check the courses added then add back unsuccessful courses";
            return Response<IEnumerable<Course>>.Fail(failMessage, (int)HttpStatusCode.ExpectationFailed);
        }

        public async Task<Response<IEnumerable<Course>>> RemoveCoursesAsync(string studentId, ICollection<Course> courses)
        {
            var response = await _unitOfWork.Student.RemoveCoursesAsync(studentId, courses);

            if (response == true)
            {
                await _unitOfWork.SaveChangesAsync();
                return Response<IEnumerable<Course>>.Success(null, "Course deletion successful");
            }
            string failMessage = "An error occured while deleting courses. Check the courses not deleted then delete again";
            return Response<IEnumerable<Course>>.Fail(failMessage, (int)HttpStatusCode.ExpectationFailed);
        }

        public async Task<Response<IEnumerable<Course>>> ReadRegisteredCoursesAsync(string studentId)
        {
            var courses = await _unitOfWork.Student.GetRegisteredCoursesAsync(studentId);

            if(courses != null)
            {
                return Response<IEnumerable<Course>>.Success(courses, "Successful");
            }
            return Response<IEnumerable<Course>>.Fail("No course has been registered");
        }
    }
}
