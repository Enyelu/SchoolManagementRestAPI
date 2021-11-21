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

                if (department != null)
                {
                    var course = _mapper.Map<Course>(courseDto);
                    course.DateModified = course.DateCreated;
                    course.Department = department;
                    course.IsActive = true;

                    await _unitOfWork.Course.AddAsync(course);
                    await _unitOfWork.SaveChangesAsync();
                    return Response<string>.Success(null, $"Successfully added {course.Name}");
                }
                return Response<string>.Fail($"Unsuccessful. {courseDto.DepartmentName} is not a valid department");
            }
            return Response<string>.Fail($"Unsuccessful. {courseDto.Name} already exist");
        }

        public async Task<Response<CourseDto>> GetCourseByNameOrCourseCodeAsync(string courseCode = null, string courseName = null)
        {
            var readCourse = await _unitOfWork.Course.GetCourseByNameOrCourseCodeAsync(courseCode, courseName);
            if (readCourse != null)
            {
                var course = _mapper.Map<CourseDto>(readCourse);
                return Response<CourseDto>.Success(course, "Success");
            }
            return Response<CourseDto>.Fail("Course not found");
        }

        public async Task<Response<string>> DeactivateCourseAsync(string courseCode = null, string courseName = null)
        {
            var course = await _unitOfWork.Course.GetCourseByNameOrCourseCodeAsync(courseCode, courseName);
            if (course != null)
            {
                course.IsActive = false;
                course.DateModified = DateTime.UtcNow.ToString();
                _unitOfWork.Course.Update(course);
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(null, $"Successfully deactivated");
            }
            return Response<string>.Fail($"Course not found");
        }

        public async Task<Response<string>> UpdateCourseAsync(CourseUpdateDto course, string CourseCode)
        {

            var searchedCourse = await _unitOfWork.Course.GetCourseByNameOrCourseCodeAsync(CourseCode);

            if(searchedCourse != null)
            {
                var mappingResponse = _mapper.Map<Course>(course);
                searchedCourse.CourseCode = mappingResponse.CourseCode ??= searchedCourse.CourseCode;
                searchedCourse.Name = mappingResponse.Name ??= searchedCourse.Name;
                searchedCourse.DateModified = DateTime.UtcNow.ToString();

                _unitOfWork.Course.Update(searchedCourse);
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(null, $"Successfully updated {course.Name}");
            }
            return Response<string>.Fail($"{course.Name} does not exist");
        }

        public async Task<Response<IEnumerable<StudentModel>>> ReadCourseStudentsAsync(string courseCode = null, string courseName = null)
        {
            var response = await _unitOfWork.Course.GetCourseByNameOrCourseCodeAsync(courseCode, courseName);

            if (response != null)
            {
                var mappingResponse = _mapper.Map<IEnumerable<StudentModel>>(response.Students);
                
                if(mappingResponse.Count() > 0)
                {
                    return Response<IEnumerable<StudentModel>>.Success(mappingResponse, "Success");
                }
                return Response<IEnumerable <StudentModel>>.Success(null, "No student offer this course");
            }
            return Response<IEnumerable<StudentModel>>.Fail($"{courseCode} does not exist");
        }

        public async Task<Response<IEnumerable<LecturerModel>>> ReadCourseLecturersAsync(string courseCode = null, string courseName = null)
        {
            var response = await _unitOfWork.Course.GetCourseByNameOrCourseCodeAsync(courseCode, courseName);
            
            if (response != null)
            {
                var mappingResponse = _mapper.Map<IEnumerable<LecturerModel>>(response.Lecturers);

                if(mappingResponse.Count() > 0)
                {
                    return Response<IEnumerable<LecturerModel>>.Success(mappingResponse, "Successful.");
                }
                return Response<IEnumerable<LecturerModel>>.Success(null, "No lecturer renders this course yet.");
            }
            return Response<IEnumerable<LecturerModel>>.Fail("course does not exist.");
        }
    }
}