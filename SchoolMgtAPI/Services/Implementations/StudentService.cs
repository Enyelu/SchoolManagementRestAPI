using AutoMapper;
using Models;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities.AppUnitOfWork;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper; 
        }
        
        public async Task<Response<ReadStudentResponseDto>> ReadStudentAsync(string studentId)
        {
            var response = await _unitOfWork.Student.GetStudentAsync(studentId);
            var studentDetail = _mapper.Map<ReadStudentResponseDto>(response);

            if (studentDetail != null)
            {
                return Response<ReadStudentResponseDto>.Success(studentDetail, "Student Found");
            }
            return Response<ReadStudentResponseDto>.Fail("Student not found");
        }

        public async Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInALevelAsync(int studentsLevel)
        {
            var students = await _unitOfWork.Student.GetAllStudentsInALevelAsync(studentsLevel);
            List<ReadStudentResponseDto> studentsDetail = new List<ReadStudentResponseDto>();

            foreach (var student in students)
            {
                var studentDetail = _mapper.Map<ReadStudentResponseDto>(student);
                studentsDetail.Add(studentDetail);
            }

            if(studentsDetail.Count == students.Count())
            {
                return Response<IEnumerable<ReadStudentResponseDto>>.Success(studentsDetail, "Successful");
            }
            return Response<IEnumerable<ReadStudentResponseDto>>.Fail("An error occured. Try again");
        }

        public async Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInADepartmentInALevelAsync(int studentsLevel, string department)
        {
            var students = await _unitOfWork.Student.GetAllStudentsInADepartmentInALevelAsync(studentsLevel, department);
            List<ReadStudentResponseDto> studentsDetail = new List<ReadStudentResponseDto>();

            foreach (var student in students)
            {
                var studentDetail = _mapper.Map<ReadStudentResponseDto>(student);
                studentsDetail.Add(studentDetail);
            }

            if (studentsDetail.Count == students.Count())
            {
                return Response<IEnumerable<ReadStudentResponseDto>>.Success(studentsDetail, "Successful");
            }
            return Response<IEnumerable<ReadStudentResponseDto>>.Fail("An error occured. Try again");
        }

        public async Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInAFacultyInALevelAsync(int studentsLevel, string faculty)
        {
            var students = await _unitOfWork.Student.GetAllStudentsInAFacultyInALevelAsync(studentsLevel, faculty);
            List<ReadStudentResponseDto> studentsDetail = new List<ReadStudentResponseDto>();

            foreach (var student in students)
            {
                var studentDetail = _mapper.Map<ReadStudentResponseDto>(student);
                studentsDetail.Add(studentDetail);
            }

            if (studentsDetail.Count == students.Count())
            {
                return Response<IEnumerable<ReadStudentResponseDto>>.Success(studentsDetail, "Successful");
            }
            return Response<IEnumerable<ReadStudentResponseDto>>.Fail("An error occured. Try again");
        }

        public async Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInDepartmentAsync(string department)
        {
            var students = await _unitOfWork.Student.GetAllStudentsInDepartmentAsync(department);
            List<ReadStudentResponseDto> studentsDetail = new List<ReadStudentResponseDto>();

            foreach (var student in students)
            {
                var studentDetail = _mapper.Map<ReadStudentResponseDto>(student);
                studentsDetail.Add(studentDetail);
            }

            if (studentsDetail.Count == students.Count())
            {
                return Response<IEnumerable<ReadStudentResponseDto>>.Success(studentsDetail, "Successful");
            }
            return Response<IEnumerable<ReadStudentResponseDto>>.Fail("An error occured. Try again");
        }

        public async Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInFacultyAsync(string faculty)
        {
            var students = await _unitOfWork.Student.GetAllStudentsInFacultyAsync(faculty);
            List<ReadStudentResponseDto> studentsDetail = new List<ReadStudentResponseDto>();

            foreach (var student in students)
            {
                var studentDetail = _mapper.Map<ReadStudentResponseDto>(student);
                studentsDetail.Add(studentDetail);
            }

            if (studentsDetail.Count == students.Count())
            {
                return Response<IEnumerable<ReadStudentResponseDto>>.Success(studentsDetail, "Successful");
            }
            return Response<IEnumerable<ReadStudentResponseDto>>.Fail("An error occured. Try again");
        }

        public async Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentAsync()
        {
            var students =  await _unitOfWork.Student.GetAllStudentsAsync();

            List<ReadStudentResponseDto> studentsDetail = new List<ReadStudentResponseDto>();

            foreach (var student in students)
            {
                var studentDetail = _mapper.Map<ReadStudentResponseDto>(student);
                studentsDetail.Add(studentDetail);
            }

            if (studentsDetail.Count == students.Count())
            {
                return Response<IEnumerable<ReadStudentResponseDto>>.Success(studentsDetail, "Successful");
            }
            return Response<IEnumerable<ReadStudentResponseDto>>.Fail("An error occured. Try again");
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

        public async Task<Response<IEnumerable<string>>> RegisterCoursesAsync(string registrationNumber, ICollection<string> courses)
        {
            var response = await _unitOfWork.Student.RegisterCoursesAsync(registrationNumber, courses);

            if(response != null)
            {
                var student = await _unitOfWork.Student.GetStudentAsync(registrationNumber);

                foreach (var course in courses)
                {
                    var searchedCourse = await _unitOfWork.Course.GetCourseByIdOrCourseCodeAsync(course);
                    searchedCourse.Students.Add(student);
                }
                await _unitOfWork.SaveChangesAsync();
                return Response<IEnumerable<string>>.Success(response, "Registration successful");
            }
            string failMessage = "An error occured while adding courses. Check the courses added then add back unsuccessful courses";
            return Response<IEnumerable<string>>.Fail(failMessage, (int)HttpStatusCode.ExpectationFailed);
        }

        public async Task<Response<IEnumerable<string>>> RemoveCoursesAsync(string registrationNumber, ICollection<string> courses)
        {
            var response = await _unitOfWork.Student.RemoveCoursesAsync(registrationNumber, courses);

            if (response)
            {
                var student = await _unitOfWork.Student.GetStudentAsync(registrationNumber);

                foreach (var course in courses)
                {
                    var searchedCourse = await _unitOfWork.Course.GetCourseByIdOrCourseCodeAsync(course);
                    searchedCourse.Students.Remove(student);
                }
                await _unitOfWork.SaveChangesAsync();
                return Response<IEnumerable<string>>.Success(courses, "Course(s) removal successful");
            }
            string failMessage = "An error occured while adding courses. Check the courses added then add back unsuccessful courses";
            return Response<IEnumerable<string>>.Fail(failMessage, (int)HttpStatusCode.ExpectationFailed);
        }

        public async Task<Response<IEnumerable<string>>> ReadRegisteredCoursesAsync(string studentId)
        {
            var courses = await _unitOfWork.Student.GetRegisteredCoursesAsync(studentId);

            if(courses != null)
            {
                return Response<IEnumerable<string>>.Success(courses, "Successful");
            }
            return Response<IEnumerable<string>>.Fail("No course has been registered");
        }
    }
}
