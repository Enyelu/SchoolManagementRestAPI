using AutoMapper;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var checkCourse = await _unitOfWork.Course.GetCourseByNameOrCourseCodeAsync(null, courseDto.CourseCode); 
            if (checkCourse == null)
            {
                var department = await _unitOfWork.Department.GetDepartmentAsync(courseDto.DepartmentName);
                var faculty = await _unitOfWork.Faculty.GetFacultyAsync(courseDto.FacultyName);

                if (department != null && faculty != null)
                {
                    var course = _mapper.Map<Course>(courseDto);
                    course.DateModified = course.DateCreated;
                    course.Department = department;
                    course.Faculty = faculty;
                    course.IsActive = true;

                    await _unitOfWork.Course.AddAsync(course);
                    await _unitOfWork.SaveChangesAsync();
                    return Response<string>.Success(null, $"Successfully added {course.Name}");
                }
                return Response<string>.Fail($"Unsuccessful. {courseDto.DepartmentName} or  {courseDto.FacultyName} is not a valid department");
            }
            return Response<string>.Fail($"Unsuccessful. {courseDto.Name} or {courseDto.CourseCode}  already exist");
        }

        public async Task<Response<CourseDto>> GetCourseByNameOrCourseCodeAsync(string courseCode = null, string courseName = null)
        {
            if (courseCode == string.Empty && courseName == string.Empty)
            {
                var readCourse = await _unitOfWork.Course.GetCourseByNameOrCourseCodeAsync(courseCode, courseName);
                if (readCourse != null)
                {
                    var course = _mapper.Map<CourseDto>(readCourse);
                    return Response<CourseDto>.Success(course, "Success");
                }
                return Response<CourseDto>.Fail("Course not found");
            }
            return Response<CourseDto>.Fail($"Course code or course name cannot be null");
        }

        public async Task<Response<string>> DeactivateCourseAsync(string courseCode = null, string courseName = null)
        {
            if (courseCode == string.Empty && courseName == string.Empty)
            {
                var course = await _unitOfWork.Course.GetCourseByNameOrCourseCodeAsync(courseCode, courseName);
                if (course != null)
                {
                    course.IsActive = false;
                    course.DateModified = DateTime.UtcNow.ToString();
                    _unitOfWork.Course.Update(course);
                    await _unitOfWork.SaveChangesAsync();
                    return Response<string>.Success(null, $"Successfully deactivated {course.Name}");
                }
                return Response<string>.Fail($"Course not found");
            }
            return Response<string>.Fail($"Course code or course name cannot be null");
        }

        public async Task<Response<string>> UpdateCourseAsync(CourseUpdateDto course, string CourseCode)
        {

            if(CourseCode == string.Empty)
            {
                var searchedCourse = await _unitOfWork.Course.GetCourseByNameOrCourseCodeAsync(CourseCode);

                if (searchedCourse != null)
                {
                    var mappingResponse = _mapper.Map<Course>(course);
                    searchedCourse.CourseCode = mappingResponse.CourseCode ?? searchedCourse.CourseCode;
                    searchedCourse.Name = mappingResponse.Name ?? searchedCourse.Name;

                    if (mappingResponse.CourseUnit != searchedCourse.CourseUnit)
                    {
                        searchedCourse.CourseUnit = mappingResponse.CourseUnit;
                    }
                    else
                    {
                        searchedCourse.CourseUnit = searchedCourse.CourseUnit;
                    }
                    searchedCourse.DateModified = DateTime.UtcNow.ToString();

                    _unitOfWork.Course.Update(searchedCourse);
                    await _unitOfWork.SaveChangesAsync();
                    return Response<string>.Success(null, $"Successfully updated {course.Name}");
                }
                return Response<string>.Fail($"{course.CourseCode} does not exist.");
            }
            return Response<string>.Fail($"Course code cannot be null");
        }

        public async Task<Response<IEnumerable<StudentResponseDto>>> ReadCourseStudentsAsync(string courseCode = null, string courseName = null)
        {
            if (courseCode == string.Empty && courseName == string.Empty)
            {
                var response = await _unitOfWork.Course.GetCourseByNameOrCourseCodeAsync(courseCode, courseName);

                if (response != null)
                {
                    var mappingResponse = _mapper.Map<IEnumerable<StudentResponseDto>>(response.Students);

                    if (mappingResponse.Count() > 0)
                    {
                        return Response<IEnumerable<StudentResponseDto>>.Success(mappingResponse, "Success");
                    }
                    return Response<IEnumerable<StudentResponseDto>>.Success(null, "No student offer this course");
                }
                return Response<IEnumerable<StudentResponseDto>>.Fail($"{courseCode} does not exist");
            }
            return Response<IEnumerable<StudentResponseDto>>.Fail("course code or course name cannot be empty");

        }

        public async Task<Response<IEnumerable<LecturerResponseModel>>> ReadCourseLecturersAsync(string courseCode = null, string courseName = null)
        {
            if (courseCode == string.Empty && courseName == string.Empty)
            {
                var response = await _unitOfWork.Course.GetCourseByNameOrCourseCodeAsync(courseCode, courseName);

                if (response != null)
                {
                    var mappingResponse = _mapper.Map<IEnumerable<LecturerResponseModel>>(response.Lecturers);

                    if (mappingResponse.Count() > 0)
                    {
                        return Response<IEnumerable<LecturerResponseModel>>.Success(mappingResponse, "Successful.");
                    }
                    return Response<IEnumerable<LecturerResponseModel>>.Success(null, "No lecturer renders this course yet.");
                }
                return Response<IEnumerable<LecturerResponseModel>>.Fail("course does not exist.");
            }
            return Response<IEnumerable<LecturerResponseModel>>.Fail("course code or course name cannot be empty");
        }
    }
}