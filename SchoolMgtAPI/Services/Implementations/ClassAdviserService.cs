using AutoMapper;
using Models;
using Repository.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<Response<ClassAdviserResponseDto>> ReadClassAdviser(int level, string department)
        {
            var classAdviser = await _unitOfWork.ClassAdviser.GetClassAdviserByDepartmentAndLevelAsync(level, department);
            
            if (classAdviser != null)
            {
               var mappedClassAdviser =  _mapper.Map<ClassAdviserResponseDto>(classAdviser);
               return Response<ClassAdviserResponseDto>.Success(mappedClassAdviser, "Successful");
            }
            return Response<ClassAdviserResponseDto>.Success(null, "No lecturer advise this level");
        }
        public async Task<Response<string>> AssignClassAdviser(string lecturerEmail, int level)
        {
            var lecturer = await _unitOfWork.Lecturer.GetLecturerDetailAsync(lecturerEmail);

            if(lecturer != null)
            {
                var newClassAdviser = ClassAdviserMapping.MapClassAdviser(level);
                newClassAdviser.Lecturer = lecturer;
                await _unitOfWork.ClassAdviser.AddAsync(newClassAdviser);
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(null, $"Successfully made {lecturerEmail}  class Adviser of level {level}");
            };
            return Response<string>.Fail($"Unsuccessfully. {lecturerEmail} is not a lecturer");
        }
    }
}