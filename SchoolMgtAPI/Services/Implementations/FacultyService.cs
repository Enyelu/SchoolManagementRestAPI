using AutoMapper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.AppUnitOfWork;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Implementations
{
    public class FacultyService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public FacultyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<string>>AddFaculty(string facultyName)
        {
            Faculty faculty = new Faculty()
            {
                Id = Guid.NewGuid().ToString(),
                Name = facultyName.Trim(),
                Date= DateTime.Now.ToString(),
                IsActive = true,
            };
           
            var result = _unitOfWork.Faculty.AddAsync(faculty);
           
            if (result.IsCompletedSuccessfully)
            {
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(facultyName, "Added successfully");
            }
            return Response<string>.Fail("Failed to add faculty");
        }

        public async Task<Response<string>> DeactivateFaculty(string facultyName)
        {
            var faculty = await _unitOfWork.Faculty.GetFacultyAsync(facultyName);
            if (faculty != null)
            {
                faculty.IsActive = false;
                return Response<string>.Success(facultyName, "Deactivated successfully");
            }
            return Response<string>.Fail($"{facultyName} not a faculty");
        }

        public async Task<Response<IEnumerable<FacultyDepartmentsResponseDto>>> ReadDepartmentsInFaculty(string facultyName)
        {
            var faculty = await _unitOfWork.Faculty.GetFacultyAsync(facultyName);

            if(faculty != null)
            {
                var result =  _mapper.Map<IEnumerable<FacultyDepartmentsResponseDto>>(faculty);
                return Response<IEnumerable<FacultyDepartmentsResponseDto>>.Success(result, "Successfully");
            }
            return Response<IEnumerable<FacultyDepartmentsResponseDto>>.Fail( "Unuccessfully.... Faculty does not exist");
        }

        public async Task<Response<IEnumerable<FacultyLecturerResponseDto>>> ReadLecturersInFaculty(string facultyName)
        {
            var faculty = await _unitOfWork.Faculty.GetFacultyAsync(facultyName);

            if (faculty != null)
            {
                var result = _mapper.Map<IEnumerable<FacultyLecturerResponseDto>>(faculty.Lecturer);
                return Response<IEnumerable<FacultyLecturerResponseDto>>.Success(result, "Successfully");
            }
            return Response<IEnumerable<FacultyLecturerResponseDto>>.Fail("Unuccessfully.... Faculty does not exist");
        }

        public async Task<Response<IEnumerable<FacultyLecturerResponseDto>>> ReadNonAcademinStaffInFaculty(string facultyName)
        {
            var faculty = await _unitOfWork.Faculty.GetFacultyAsync(facultyName);

            if (faculty != null)
            {
                var result = _mapper.Map<IEnumerable<FacultyLecturerResponseDto>>(faculty.NonAcademicStaff);
                return Response<IEnumerable<FacultyLecturerResponseDto>>.Success(result, "Successfully");
            }
            return Response<IEnumerable<FacultyLecturerResponseDto>>.Fail("Unuccessfully.... Faculty does not exist");
        }
    }
}
