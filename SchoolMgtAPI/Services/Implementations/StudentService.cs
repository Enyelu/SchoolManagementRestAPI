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
        
        public async Task<Response<ReadStudentResponseDto>> ReadStudentAsync(string resgitrationNumber)
        {
            var response = await _unitOfWork.Student.GetStudentAsync(resgitrationNumber);
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

            var studentDetail = _mapper.Map<IEnumerable<ReadStudentResponseDto>>(students);
           
            return Response<IEnumerable<ReadStudentResponseDto>>.Success(studentsDetail, "Successful");
        }

        public async Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInADepartmentInALevelAsync(int studentsLevel, string department)
        {
            var students = await _unitOfWork.Student.GetAllStudentsInADepartmentInALevelAsync(studentsLevel, department);

            if(students != null)
            {
                var studentsDetail = _mapper.Map<IEnumerable<ReadStudentResponseDto>>(students);
                return Response<IEnumerable<ReadStudentResponseDto>>.Success(studentsDetail, "Successful");
            }

            return Response<IEnumerable<ReadStudentResponseDto>>.Fail("An error occured. Try again");
        }

        public async Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInAFacultyInALevelAsync(int studentsLevel, string faculty)
        {
            var students = await _unitOfWork.Student.GetAllStudentsInAFacultyInALevelAsync(studentsLevel, faculty);
           
            if(students != null)
            {
                var studentsDetail = _mapper.Map<IEnumerable<ReadStudentResponseDto>>(students);
                return Response<IEnumerable<ReadStudentResponseDto>>.Success(studentsDetail, "Successful");
            }
                
            return Response<IEnumerable<ReadStudentResponseDto>>.Fail("An error occured. Try again");
        }

        public async Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInDepartmentAsync(string department)
        {
            var students = await _unitOfWork.Student.GetAllStudentsInDepartmentAsync(department);

            if(students != null)
            {
                var studentsDetail = _mapper.Map<IEnumerable<ReadStudentResponseDto>>(students);
                return Response<IEnumerable<ReadStudentResponseDto>>.Success(studentsDetail, "Successful");
            }
            
            return Response<IEnumerable<ReadStudentResponseDto>>.Fail("An error occured. Try again");
        }

        public async Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInFacultyAsync(string faculty)
        {
            var students = await _unitOfWork.Student.GetAllStudentsInFacultyAsync(faculty);

            if(students != null)
            {
                var studentsDetail = _mapper.Map<IEnumerable<ReadStudentResponseDto>>(students);
                return Response<IEnumerable<ReadStudentResponseDto>>.Success(studentsDetail, "Successful");
            }
            return Response<IEnumerable<ReadStudentResponseDto>>.Fail("An error occured. Try again");
        }

        public async Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentAsync()
        {
            var students =  await _unitOfWork.Student.GetAllStudentsAsync();

            if(students != null)
            {
                var studentsDetail = _mapper.Map<IEnumerable<ReadStudentResponseDto>>(students);
                return Response<IEnumerable<ReadStudentResponseDto>>.Success(studentsDetail, "Successful");
            }
            return Response<IEnumerable<ReadStudentResponseDto>>.Fail("An error occured. Try again");
        }

        public async Task<Response<string>> DeactivateStudentAsync(string registrationNumber)
        {
            var student = await _unitOfWork.Student.GetStudentAsync(registrationNumber);
            
            if(student != null)
            {
                student.AppUser.IsActive = false;
                _unitOfWork.Student.Update(student);
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(null, "Student deactivated successfully");
            }
            return Response<string>.Fail("Student not found");
        }
        public async Task<Response<string>> CheckStudentIsActiveAsync(string registrationNumber)
        {
            var student = await _unitOfWork.Student.GetStudentAsync(registrationNumber);
            var studentStatus = student.AppUser.IsActive;
            
            if(studentStatus)
            {
                return Response<string>.Success(null, "Student is active");
            }
            
            return Response<string>.Fail("Student is not active");
        }

        public async Task<Response<IEnumerable<Course>>> RegisterCoursesAsync(string studentId, ICollection<string> courses)
        {
            var student = await _unitOfWork.Student.GetStudentAsync(null, studentId);

            if (student != null)
            {
                var courseCount = student.Courses.Count();
                foreach (var course in courses)
                {
                    var searchedCourse = await _unitOfWork.Course.GetCourseByIdOrCourseCodeAsync(course);
                    searchedCourse.Name = course;
                    student.Courses.Add(searchedCourse);
                    searchedCourse.Students.Add(student);
                    _unitOfWork.Student.Update(student);
                    _unitOfWork.Course.Update(searchedCourse);
                }
                await _unitOfWork.SaveChangesAsync();
                var result = student.Courses.ToList();
                return Response<IEnumerable<Course>>.Success(result, "Registration successful")  ;
            }
            return Response<IEnumerable<Course>>.Fail("An error occured while adding courses.", (int)HttpStatusCode.ExpectationFailed);
        }
       
        public async Task<Response<IEnumerable<string>>> RemoveCoursesAsync(string studentId, ICollection<string> courses)
        {

            var student = await _unitOfWork.Student.GetStudentAsync(null, studentId);
            var registeredCourses = student.Courses;

            if(student != null)
            {
                foreach (var course in courses)
                {
                    var searchedCourse = await _unitOfWork.Course.GetCourseByIdOrCourseCodeAsync(course);
                   
                    if (registeredCourses.Contains(searchedCourse))
                    {
                        registeredCourses.Remove(searchedCourse);
                        searchedCourse.Students.Remove(student);
                        _unitOfWork.Student.Update(student);
                        _unitOfWork.Course.Update(searchedCourse);
                    }
                }
                await _unitOfWork.SaveChangesAsync();
                return Response<IEnumerable<string>>.Success(courses, "Course(s) removal successful");
            }
            return Response<IEnumerable<string>>.Fail("An error occured while removing courses.", (int)HttpStatusCode.ExpectationFailed);
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
