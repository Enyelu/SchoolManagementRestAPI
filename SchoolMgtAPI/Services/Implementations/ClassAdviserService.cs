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
        public async Task<Response<ClassAdviserResponseDto>> ReadClassAdviserAsync(int level, string department)
        {
            if(level.ToString() == string.Empty || department == string.Empty)
            {
                var classAdviser = await _unitOfWork.ClassAdviser.GetClassAdviserByDepartmentAndLevelAsync(level, department);

                if (classAdviser != null)
                {
                    var mappedClassAdviser = _mapper.Map<ClassAdviserResponseDto>(classAdviser);
                    return Response<ClassAdviserResponseDto>.Success(mappedClassAdviser, "Successful");
                }
                return Response<ClassAdviserResponseDto>.Success(null, "No lecturer advise this level");
            }
            return Response<ClassAdviserResponseDto>.Fail("level or department cannot be empty");
        }
        public async Task<Response<string>> AssignClassAdviserAsync(string lecturerEmail, int level)
        {
            if(lecturerEmail == string.Empty || level.ToString() == string.Empty)
            {
                var lecturer = await _unitOfWork.Lecturer.GetLecturerDetailAsync(lecturerEmail);

                if (lecturer != null)
                {
                    var newClassAdviser = ClassAdviserMapping.MapClassAdviser(level);
                    newClassAdviser.Lecturer = lecturer;
                    await _unitOfWork.ClassAdviser.AddAsync(newClassAdviser);
                    await _unitOfWork.SaveChangesAsync();
                    return Response<string>.Success(string.Empty, $"Successfully made {lecturerEmail}  class Adviser of level {level}");
                };
                return Response<string>.Fail($"Unsuccessfully. {lecturerEmail} is not a lecturer");
            }
            return Response<string>.Fail($"lecturer's email or level cannot be string.Empty");
        }

        public async Task<Response<string>> DeactivateClassAdviserAsync(int level, string department)
        {
            if(level.ToString() == string.Empty || department == string.Empty)
            {
                var classAdviser = await _unitOfWork.ClassAdviser.GetClassAdviserByDepartmentAndLevelAsync(level, department);

                if (classAdviser != null)
                {
                    classAdviser.IsCourseAdviser = false;
                    _unitOfWork.ClassAdviser.Update(classAdviser);
                    await _unitOfWork.SaveChangesAsync();
                    return Response<string>.Success(string.Empty, $"Successfully deactivated {classAdviser.Lecturer.AppUser.Email}  as class Adviser of level {level}");
                };
                return Response<string>.Fail($"Unsuccessfully. No class adviser for this level");
            }
            return Response<string>.Fail("level or department cannot be empty");
        }
    }
}


