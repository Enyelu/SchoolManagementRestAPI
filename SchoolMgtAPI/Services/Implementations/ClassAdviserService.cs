using AutoMapper;
using Services.Interfaces;
using System.Threading.Tasks;
using Utilities.AppUnitOfWork;
using Utilities.Dtos;
using Utilities.GeneralResponse;
using Utilities.Mappings;

namespace Services.Implementations
{
    public class ClassAdviserService : IClassAdviserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ClassAdviserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Response<ClassAdviserResponseDto>> ReadClassAdviserAsync(ReadClassAdviserDto requestDto)
        {
            var classAdviser = await _unitOfWork.ClassAdviser.GetClassAdviserByDepartmentAndLevelAsync(requestDto.Level, requestDto.Department);

            if (classAdviser != null)
            {
                var mappedClassAdviser = _mapper.Map<ClassAdviserResponseDto>(classAdviser);
                return Response<ClassAdviserResponseDto>.Success(mappedClassAdviser, "Successful");
            }
            return Response<ClassAdviserResponseDto>.Success(null, "No lecturer advise this level");
        }
        public async Task<Response<string>> AssignClassAdviserAsync(AssignClassAdviserDto requestDto)
        {
            var lecturer = await _unitOfWork.Lecturer.GetLecturerDetailAsync(requestDto.LecturerEmail);

            if (lecturer != null)
            {
                var newClassAdviser = ClassAdviserMapping.MapClassAdviser(requestDto.Level);
                newClassAdviser.Lecturer = lecturer;
                await _unitOfWork.ClassAdviser.AddAsync(newClassAdviser);
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(string.Empty, $"Successfully made {requestDto.LecturerEmail}  class Adviser of level {requestDto.Level}");
            };
            return Response<string>.Fail($"Unsuccessfully. {requestDto.LecturerEmail} is not a lecturer");
        }

        public async Task<Response<string>> DeactivateClassAdviserAsync(ReadClassAdviserDto requestDto)
        {
            var classAdviser = await _unitOfWork.ClassAdviser.GetClassAdviserByDepartmentAndLevelAsync(requestDto.Level, requestDto.Department);

            if (classAdviser != null)
            {
                classAdviser.IsCourseAdviser = false;
                _unitOfWork.ClassAdviser.Update(classAdviser);
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(string.Empty, $"Successfully deactivated {classAdviser.Lecturer.AppUser.Email}  as class Adviser of level {requestDto.Level}");
            };
            return Response<string>.Fail($"Unsuccessfully. No class adviser for this level");
        }
    }
}


