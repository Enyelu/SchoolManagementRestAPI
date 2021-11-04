using AutoMapper;
using Models;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.AppUnitOfWork;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                return Response<Course>.Success(course, "Success");
            }
            return Response<Course>.Fail("Course not found");
        }
        public async Task<Response<string>> DeactivateCourseAsync(string courseCode = null, string courseId = null)
        {
            var response = await _unitOfWork.Course.DeactivateCourseAsync(courseCode, courseId);
            if (response == false)
            {
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(null, $"Successfully updated deactivated");
            }
            return Response<string>.Fail($"Successfully updated deactivated");

        }

        public async Task<Response<string>> UpdateCourseAsync(CourseUpdateDto course)
        {

            var searchedCourse = await _unitOfWork.Course.GetCourseByIdOrCourseCodeAsync(course.CourseCode);

            if(searchedCourse != null)
            {
                var mappingResponse = _mapper.Map<Course>(course);
                searchedCourse.CourseCode = mappingResponse.CourseCode;
                searchedCourse.Name = mappingResponse.Name;

                _unitOfWork.Course.Update(searchedCourse);
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(null, $"Successfully updated {course.Name}");
            }
            return Response<string>.Fail($"{course.Name} does not exist");
        }

        public async Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadCourseStudents(string courseCode = null, string courseId = null)
        {
            var studentsList = new List<ReadStudentResponseDto>();
            var response = await _unitOfWork.Course.CourseStudents(courseCode, courseId);

            if (response != null)
            {
                var mappingResponse = _mapper.Map<StudentModel>(response);

                foreach (var student in mappingResponse.Students)
                {
                    var singleStudent = _mapper.Map<ReadStudentResponseDto>(student);
                    studentsList.Add(singleStudent);
                    return Response<IEnumerable < ReadStudentResponseDto >>.Success(studentsList, "Success");
                }
            }
            return Response<IEnumerable<ReadStudentResponseDto>>.Fail($"No student offer {courseCode}"); ;
        }

        public async Task<Response<IEnumerable<CourseLecturerResponseDto>>> ReadCourseLecturers(string courseCode = null, string courseId = null)
        {
            var LecturerList = new List<CourseLecturerResponseDto>();
            var response = await _unitOfWork.Course.CourseLecturers(courseCode, courseId);

            if (response != null)
            {
                var mappingResponse = _mapper.Map<LecturerModel>(response);
                
                foreach (var lecturer in mappingResponse.Lecturer)
                {
                    var lecturerInmappingResponse = new CourseLecturerResponseDto
                    {
                        FullName = $" {lecturer.AppUser.FirstName} {lecturer.AppUser.MiddleName} {lecturer.AppUser.LastName}",
                        Avatar = lecturer.AppUser.Avatar,
                        IsActive = lecturer.AppUser.IsActive,
                        BirthDate = lecturer.AppUser.BirthDate,
                        DateCreated = lecturer.AppUser.DateCreated,
                        StreetNumber = lecturer.AppUser.Addresses.StreetNumber,
                        City = lecturer.AppUser.Addresses.City,
                        State = lecturer.AppUser.Addresses.State,
                        Country = lecturer.AppUser.Addresses.Country
                    };
                    LecturerList.Add(lecturerInmappingResponse);
                }
                return Response<IEnumerable<CourseLecturerResponseDto>>.Success(LecturerList, "Successful.");
            }
            return Response<IEnumerable<CourseLecturerResponseDto>>.Fail("No lecturer renders this course yet.");
        }
    }
}
