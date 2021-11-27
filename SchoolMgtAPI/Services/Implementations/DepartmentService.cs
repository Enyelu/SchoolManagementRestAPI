using AutoMapper;
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


        public async Task<Response<string>> AddDepartmentAsync(DepartmentRequestDto requestDto)
        {  
            var checkDepartment = await _unitOfWork.Department.GetDepartmentAsync(requestDto.DepartmentName);

            if (checkDepartment == null)
            {
                var department = DepartmentMap.DepartmentMapping(requestDto.DepartmentName);
                var readFaculty = await _unitOfWork.Faculty.GetFacultyAsync(requestDto.FacultyName);

                if (readFaculty != null)
                {
                    department.Faculty = readFaculty;
                    await _unitOfWork.Department.AddAsync(department);
                    await _unitOfWork.SaveChangesAsync();
                    return Response<string>.Success(null, $"Successfully created {requestDto.DepartmentName} department and added to {requestDto.FacultyName} faculty");
                }
                return Response<string>.Fail($"Unsuccessfully. Faculty: {requestDto.FacultyName}, does not exist");
            }
            return Response<string>.Fail($"Unsuccessfully. Department: {requestDto.DepartmentName}, already exist");
        }

        public async Task<bool> DeactivateDepartmentAsync( NameDto departmentName)
        {
          
            var department = await _unitOfWork.Department.GetDepartmentAsync(departmentName.Name);

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

        public async Task<Response<string>> AddLecturerToDepartmentAsync(AddLecturerRequestDto requestDto)
        {
                var lecturer = await _unitOfWork.Lecturer.GetLecturerDetailAsync(requestDto.LecturerEmail);
                var ReadDepartment = await _unitOfWork.Department.GetDepartmentAsync(requestDto.DepartmentName);

                if (lecturer != null)
                {
                    if (ReadDepartment != null)
                    {
                        lecturer.Department = ReadDepartment;
                        _unitOfWork.Lecturer.Update(lecturer);
                        await _unitOfWork.SaveChangesAsync();
                        return Response<string>.Success(null, $"Lecturer added successfully to {requestDto.DepartmentName} department");
                    }
                    return Response<string>.Fail("Department does not exist");
                }
                return Response<string>.Fail("Invalid Email");
        }

        public async Task<Response<string>> DeactivateLecturerFromDepartmentAsync(EmailRequestDto lecturerEmail)
        {
            var lecturer = await _unitOfWork.Lecturer.GetLecturerDetailAsync(lecturerEmail.Email);
            if (lecturer != null)
            {
                lecturer.Department.IsActive = false;
                _unitOfWork.Lecturer.Update(lecturer);
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(null, "Lecturer deactivated successfully");
            }
            return Response<string>.Fail("Lecturer deactivation not successfully");
        }

        public async Task<Response<IEnumerable<LecturerResponseDto>>> GetAllLecturersInADepartmentAsync(NameDto departmentName)
        {
            var department = await _unitOfWork.Department.GetDepartmentAsync(departmentName.Name);
            var departmentLecturers = department.Lecturer.Where(x => x.AppUser.IsActive = true);

            if (department != null)
            {
                if (departmentLecturers != null)
                {
                    var lecturers = _mapper.Map<IEnumerable<LecturerResponseDto>>(departmentLecturers);
                    return Response<IEnumerable<LecturerResponseDto>>.Success(lecturers, "successfully");
                }
                return Response<IEnumerable<LecturerResponseDto>>.Success(null, "No lecturer exist here at the moment");
            }
            return Response<IEnumerable<LecturerResponseDto>>.Fail("department does not exist");
          
        }

        public async Task<Response<IEnumerable<CourseDto>>> GetDeparmentCoursesAsync(NameDto departmentName)
        {
           
            var department = await _unitOfWork.Department.GetDepartmentAsync(departmentName.Name);

            if (department != null)
            {
                var departmentCourses = department.Courses.Where(x => x.IsActive = true);
                var courses = _mapper.Map<IEnumerable<CourseDto>>(departmentCourses);
                return Response<IEnumerable<CourseDto>>.Success(courses, "Successful");
            }
            return Response<IEnumerable<CourseDto>>.Fail("Department does not exit");
           
        }
    }
}