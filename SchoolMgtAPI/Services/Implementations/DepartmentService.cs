﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Models;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities.AppUnitOfWork;
using Utilities.Dtos;
using Utilities.GeneralResponse;
using Utilities.Mappings;

namespace Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        

        public DepartmentService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork; 
            _mapper = mapper;
        }


        public async Task<Response<string>> AddDepartmentAsync(string departmentName, string facultyName)
        {  
            var checkDepartment = await _unitOfWork.Department.GetDepartmentAsync(departmentName);

            if(checkDepartment == null)
            {
                var department = DepartmentMap.DepartmentMapping(departmentName);
                var readFaculty = await _unitOfWork.Faculty.GetFacultyAsync(facultyName);

                if (readFaculty != null)
                {
                    department.Faculty = readFaculty;
                    await _unitOfWork.Department.AddAsync(department);
                    await _unitOfWork.SaveChangesAsync();
                    return Response<string>.Success(null, $"Successfully created {departmentName} department and added to {facultyName} faculty");
                }
                return Response<string>.Fail($"Unsuccessfully. Faculty: {facultyName}, does not exist");
            }
            return Response<string>.Fail($"Unsuccessfully. Department: {departmentName}, already exist");
        }

        public async Task<bool> DeactivateDepartmentAsync( string departmentName)
        {
           var department = await _unitOfWork.Department.GetDepartmentAsync(departmentName);

            if (department != null)
            {
                department.IsActive = false;
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Response<IEnumerable<ReadDepartmentDto>>> ReadAllDepartmentsAsync()
        {
            var departments = await _unitOfWork.Department.GetAllDepartmentDetailsAsync();
            var activeDepartmentc = departments.Where(d => d.IsActive = true);

            if (activeDepartmentc.Count() > 0)
            {
                var mappedDepartments = _mapper.Map<IEnumerable<ReadDepartmentDto>>(activeDepartmentc);
                return Response<IEnumerable<ReadDepartmentDto>>.Success(mappedDepartments, "Successful");
            }
            return Response<IEnumerable<ReadDepartmentDto>>.Fail("UnSuccessful");
        }

        public async Task<Response<string>> AddLecturerToDepartmentAsync(string lecturerEmail, string departmentName)
        {
            var lecturer = await _unitOfWork.Lecturer.GetLecturerDetailAsync(lecturerEmail);
            var ReadDepartment = await _unitOfWork.Department.GetDepartmentAsync(departmentName);
            
            if(lecturer != null)
            {
                if (ReadDepartment != null)
                {
                    lecturer.Department = ReadDepartment;
                    _unitOfWork.Lecturer.Update(lecturer);
                    await _unitOfWork.SaveChangesAsync();
                    return Response<string>.Success(null, $"Lecturer added successfully to {departmentName} department");
                }
                return Response<string>.Fail("Department does not exist");
            }
            return Response<string>.Fail("Invalid Email");
        }

        public async Task<Response<string>> DeactivateLecturerFromDepartmentAsync(string lecturerEmail)
        {
            var lecturer = await _unitOfWork.Lecturer.GetLecturerDetailAsync(lecturerEmail);
            if (lecturer != null)
            {
                lecturer.Department.IsActive = false;
                _unitOfWork.Lecturer.Update(lecturer);
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(null, "Lecturer deactivated successfully");
            }
            return Response<string>.Fail("Lecturer deactivation not successfully");
        }

        public async Task<Response<IEnumerable<LecturerResponseDto>>> GetAllLecturersInADepartmentAsync(string departmentName)
        {
            var department = await _unitOfWork.Department.GetDepartmentAsync(departmentName);
            var departmentLecturers = department.Lecturer.Where(x => x.AppUser.IsActive = true);
            
            if (department != null)
            {
                if(departmentLecturers != null)
                {
                    var lecturers = _mapper.Map<IEnumerable<LecturerResponseDto>>(departmentLecturers);
                    return Response<IEnumerable<LecturerResponseDto>>.Success(lecturers, "successfully");
                }
                return Response<IEnumerable<LecturerResponseDto>>.Success(null, "No lecturer exist here at the moment");
            }
            return Response<IEnumerable<LecturerResponseDto>>.Fail("department does not exist");
        }

        public async Task<Response<IEnumerable<CourseDto>>> GetDeparmentCoursesAsync(string departmentName)
        {
            var department = await _unitOfWork.Department.GetDepartmentAsync(departmentName);
            
            if (department != null)
            {
                var departmentCourses = department.Courses.Where(x => x.IsActive = true);
                var courses = _mapper.Map<IEnumerable<CourseDto>>(departmentCourses);
                return Response<IEnumerable<CourseDto>>.Success(courses, "Successful");
            }
            return Response<IEnumerable<CourseDto>>.Fail("Department does not exit");
        }

        //public async Task<Response<string>> AddCourseToDepartmentAsync(string departmentName, string courseCode)
        //{
        //    var course = await _unitOfWork.Course.GetCourseByNameOrCourseCodeAsync(courseCode);
        //    if (course != null)
        //    {
        //        var department = await _unitOfWork.Department.GetDepartmentAsync(departmentName);

        //        if(department != null)
        //        {
        //            department.Courses.Add(course);
        //            _unitOfWork.Department.Update(department);
        //            await _unitOfWork.SaveChangesAsync();
        //            return Response<string>.Success(null, $"{course.Name} {(courseCode)} added successfully");
        //        }
        //        return Response<string>.Fail("department does not exist");
        //    }
        //    return Response<string>.Fail("Course does not exist");
        //}

        //public async Task<Response<string>> DeactivateDepartmentCourseAsycn(string departmentName, string courseCode)
        //{
        //    var course = await _unitOfWork.Course.GetCourseByNameOrCourseCodeAsync(courseCode);
        //    if (course != null)
        //    {
        //        var department = await _unitOfWork.Department.GetDepartmentAsync(departmentName);

        //        if (department != null)
        //        {
        //            var readCourse = department.Courses.FirstOrDefault(x => x.CourseCode.ToLower().Trim() == courseCode.ToLower().Trim() ).IsActive = false;
        //            _unitOfWork.Department.Update(department);
        //            await _unitOfWork.SaveChangesAsync();
        //            return Response<string>.Success(null, $"{course.Name} {(courseCode)} deactivated successfully");
        //        }
        //        return Response<string>.Fail("department does not exist");
        //    }
        //    return Response<string>.Fail("Course does not exist");
        //}
    }
}