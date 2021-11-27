using AutoMapper;
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
    public class FacultyService : IFacultyService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public FacultyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<string>>AddFaculty(NameDto requestDto)
        {
            var checkFaculty = await _unitOfWork.Faculty.GetFacultyAsync(requestDto.Name);

            if (checkFaculty == null)
            {
                var faculty = FacultyMap.FacultyMapping(requestDto.Name);
                await _unitOfWork.Faculty.AddAsync(faculty);
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(requestDto.Name, "Added successfully");
            }
            return Response<string>.Fail($"Failed. Faculty with name: {requestDto.Name} already exist");
        }

        public async Task<Response<string>> DeactivateFacultyAsync(NameDto requestDto)
        {
            var faculty = await _unitOfWork.Faculty.GetFacultyAsync(requestDto.Name);
            if (faculty != null)
            {
                faculty.IsActive = false;
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(requestDto.Name, "Deactivated successfully");
            }
            return Response<string>.Fail($"{requestDto} not a faculty");
        }

        public async Task<Response<IEnumerable<FacultyDepartmentsResponseDto>>> ReadDepartmentsInFacultyAsync(NameDto requestDto)
        {
                var faculty = await _unitOfWork.Faculty.GetFacultyAsync(requestDto.Name);

                if (faculty != null)
                {
                    var result = _mapper.Map<IEnumerable<FacultyDepartmentsResponseDto>>(faculty.Departments);
                    var trueResult = result.Where(x => x.IsActive == true);
                    return Response<IEnumerable<FacultyDepartmentsResponseDto>>.Success(trueResult, "Successfully");
                }
                return Response<IEnumerable<FacultyDepartmentsResponseDto>>.Fail("Unuccessfully.... Faculty does not exist");
        }

        public async Task<Response<IEnumerable<FacultyLecturerResponseDto>>> ReadLecturersInFacultyAsync(NameDto reuestDto)
        {
            var faculty = await _unitOfWork.Faculty.GetFacultyAsync(reuestDto.Name);

            if (faculty != null)
            {
                var result = _mapper.Map<IEnumerable<FacultyLecturerResponseDto>>(faculty.Lecturer);
                return Response<IEnumerable<FacultyLecturerResponseDto>>.Success(result, "Successfully");
            }
            return Response<IEnumerable<FacultyLecturerResponseDto>>.Fail("Unuccessfully.... Faculty does not exist");
        }

        public async Task<Response<IEnumerable<FacultyLecturerResponseDto>>> ReadNonAcademinStaffInFacultyAsync(NameDto requestDto)
        {
            var faculty = await _unitOfWork.Faculty.GetFacultyAsync(requestDto.Name);

            if (faculty != null)
            {
                var result = _mapper.Map<IEnumerable<FacultyLecturerResponseDto>>(faculty.NonAcademicStaff);
                return Response<IEnumerable<FacultyLecturerResponseDto>>.Success(result, "Successfully");
            }
            return Response<IEnumerable<FacultyLecturerResponseDto>>.Fail("Unuccessfully.... Faculty does not exist");
        }
    }
}
