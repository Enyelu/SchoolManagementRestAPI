using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Models;
using Services.Interfaces;
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
    public class LecturerService : ILecturerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public LecturerService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Response<LecturerResponseDto>> ReadLecturerDetailAsync(string lecturerEmail)
        {
            var lecturer = await _userManager.FindByEmailAsync(lecturerEmail);
            
            if (lecturer == null)
            {
                var mappedLecturer = _mapper.Map<LecturerResponseDto>(lecturer);
                return Response<LecturerResponseDto>.Success(mappedLecturer, "Successful");
            }
            return Response<LecturerResponseDto>.Fail("Unsuccessful. Lecturer not found");
        }

        public async Task<Response<string>> DeactivateLecturerAsync(string lecturerEmail)
        {
            var lecturer = await _userManager.FindByEmailAsync(lecturerEmail);
            
            if(lecturer != null)
            {
                lecturer.IsActive = false;
                var result = _userManager.UpdateAsync(lecturer);

                if(result.IsCompleted)
                {
                    await _unitOfWork.SaveChangesAsync();
                    return Response<string>.Success(null, "Deactivation was successful");
                }
                return Response<string>.Fail("an error occured while attempting to Deactivate lecturer");
            }
            return Response<string>.Fail("Unsuccessful. Lecturer not found");
        }

        public async Task<Response<string>> UpdateLecturerAsync(LecturerDto lecturerDto, string lecturerEmail)
        {
            var lecturer = await _userManager.FindByEmailAsync(lecturerEmail);
            lecturer.FirstName =  lecturerDto.FirstName;
            lecturer.MiddleName = lecturerDto.MiddleName;
            lecturer.LastName = lecturerDto.LastName;
            lecturer.Addresses.StreetNumber = lecturerDto.StreetNumber;
            lecturer.Addresses.City = lecturerDto.City;
            lecturer.Addresses.State = lecturerDto.State;   
            lecturer.Addresses.Country = lecturerDto.Country;

            var result =  await _userManager.UpdateAsync(lecturer);

            if(result.Succeeded)
            {
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(null, $"{lecturer.FirstName} {lecturer.LastName} updated successfully");
            }
            else
            {
                return Response<string>.Fail("an error occured while attempting to update lecturer");
            }
        }
    }
}
