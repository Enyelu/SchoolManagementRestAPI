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

        public async Task<Response<string>> AddCourseAsync(CourseDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);

            await _unitOfWork.Course.AddAsync(course);
            await _unitOfWork.SaveChangesAsync();
            return Response<string>.Success(null, $"Successfully added {course.Name}");
        }

        public async Task<Response<CourseDto>> GetCourseByIdOrCourseCodeAsync(string courseCode = null, string courseId = null)
        {
            var readCourse = await _unitOfWork.Course.GetCourseByIdOrCourseCodeAsync(courseCode, courseId);
            if (readCourse == null)
            {
                var course = _mapper.Map<CourseDto>(readCourse);
                return Response<CourseDto>.Success(course, "Success");
            }
            return Response<CourseDto>.Fail("Course not found");
        }

        public async Task<Response<string>> DeactivateCourseAsync(string courseCode = null, string courseId = null)
        {
            var course = await _unitOfWork.Course.GetCourseByIdOrCourseCodeAsync(courseCode, courseId);
            if (course != null)
            {
                course.IsActive = false;
                _unitOfWork.Course.Update(course);
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(null, $"Successfully deactivated");
            }
            return Response<string>.Fail($"Course not found");
        }

        public async Task<Response<string>> UpdateCourseAsync(CourseUpdateDto course)
        {

            var searchedCourse = await _unitOfWork.Course.GetCourseByIdOrCourseCodeAsync(course.CourseCode);

            if(searchedCourse != null)
            {
                var mappingResponse = _mapper.Map<Course>(course);
                searchedCourse.CourseCode = mappingResponse.CourseCode ??= searchedCourse.CourseCode;
                searchedCourse.Name = mappingResponse.Name ??= searchedCourse.Name;

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
                var singleStudent = _mapper.Map<IEnumerable<ReadStudentResponseDto>>(mappingResponse);
                return Response<IEnumerable < ReadStudentResponseDto >>.Success(studentsList, "Success");
            }
            return Response<IEnumerable<ReadStudentResponseDto>>.Fail($"No student offer {courseCode}"); ;
        }

        public async Task<Response<IEnumerable<CourseLecturerResponseDto>>> ReadCourseLecturers(string courseCode = null, string courseId = null)
        {
            var LecturerList = new List<CourseLecturerResponseDto>();
            var response = await _unitOfWork.Course.CourseLecturers(courseCode, courseId);
            
            if (response != null)
            {
                var mappingResponse = _mapper.Map<IEnumerable<LecturerModel>>(response);
                return Response<IEnumerable<CourseLecturerResponseDto>>.Success(LecturerList, "Successful.");
            }
            return Response<IEnumerable<CourseLecturerResponseDto>>.Fail("No lecturer renders this course yet.");
        }
    }
}
