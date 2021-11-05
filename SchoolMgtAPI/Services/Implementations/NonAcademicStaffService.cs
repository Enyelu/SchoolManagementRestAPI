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

namespace Services.Implementations
{
    public class NonAcademicStaffService : INonAcademicStaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public NonAcademicStaffService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<Response<NonAcademicStaffResponseDto>> ReadNonAcademicStaffAsync(string staffId)
        {
            var readStaff = await _unitOfWork.NonAcademicStaff.GetNonAcademicStaffAsync(staffId);
            var staff = _mapper.Map<NonAcademicStaffResponseDto>(readStaff);

            if(staff != null)
            {
                return Response<NonAcademicStaffResponseDto>.Success(staff, "Successful");
            }
            return Response<NonAcademicStaffResponseDto>.Fail("Unsuccessful. Staff does not exist");
        }

        public async Task<Response<IEnumerable<NonAcademicStaffResponseDto>>> ReadAllNonAcademicStaffAsync()
        {
            var readListOfStaff = await _unitOfWork.NonAcademicStaff.GetAllNonAcademicStaffAsync();
            var listOfStaff = new List<NonAcademicStaffResponseDto>();

            if (readListOfStaff != null)
            {
                foreach (var staff in readListOfStaff)
                {
                    var mappedStaff = _mapper.Map<NonAcademicStaffResponseDto>(staff);
                    listOfStaff.Add(mappedStaff);
                }
                if(readListOfStaff.Count().Equals(listOfStaff.Count()))
                {
                    return Response<IEnumerable<NonAcademicStaffResponseDto>>.Success(listOfStaff, "Successful");

                }
            }
            return Response<IEnumerable<NonAcademicStaffResponseDto>>.Fail("Unsuccessful. Try again....");
        }

        public async Task<Response<string>> DeactivateNonAcademicStaffAsync(string staffId)
        {
            var readStaff = await _unitOfWork.NonAcademicStaff.GetNonAcademicStaffAsync(staffId);
            
            if (readStaff != null)
            {
                readStaff.AppUser.IsActive = false;
                _unitOfWork.NonAcademicStaff.Update(readStaff);
                await _unitOfWork.SaveChangesAsync();
                
                var responseString = $"{readStaff.AppUser.FirstName} {readStaff.AppUser.LastName} was deactivated";
                return Response<string>.Success(null, responseString);
            }
            return Response<string>.Fail("Deactivation was unsuccessful");
        }


        public async Task<Response<string>> UpdateNonAcademicStaffAsync(NonAcademicStaffDto staff, string staffId)
        {
            var readStaff = await _userManager.FindByIdAsync(staffId);
           
            if(readStaff != null)
            {
                readStaff.FirstName = staff.FirstName;
                readStaff.MiddleName = staff.MiddleName;
                readStaff.LastName = staff.LastName;
                readStaff.Addresses.StreetNumber = staff.StreetNumber ;
                readStaff.Addresses.City = staff.City;
                readStaff.Addresses.State = staff.State;
                readStaff.Addresses.Country = staff.Country;

                var a =await _userManager.UpdateAsync(readStaff);
                if(a.Succeeded)
                {
                    await _unitOfWork.SaveChangesAsync();
                    return Response<string>.Success(null, $"Update was successful");
                }
            }
            return Response<string>.Fail("Update was unsuccessful");
        }
    }
}
