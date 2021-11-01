using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.AppUnitOfWork;
using Utilities.GeneralResponse;

namespace Services.Implementations
{
    public class NonAcademicStaffService : INonAcademicStaffService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NonAcademicStaffService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<NonAcademicStaff>> ReadNonAcademicStaffAsync(string staffId)
        {
            var staff = await _unitOfWork.NonAcademicStaff.GetNonAcademicStaffAsync(staffId);

            if(staff != null)
            {
                return Response<NonAcademicStaff>.Success(staff, "Successful");
            }
            return Response<NonAcademicStaff>.Fail("Unsuccessful. Staff does not exist");
        }

        public async Task<Response<IEnumerable<NonAcademicStaff>>> ReadAllNonAcademicStaffAsync()
        {
            var listOfStaff = await _unitOfWork.NonAcademicStaff.GetAllNonAcademicStaffAsync();

            if(listOfStaff != null)
            {
                return Response<IEnumerable<NonAcademicStaff>>.Success(listOfStaff, "Successful");
            }
            return Response<IEnumerable<NonAcademicStaff>>.Fail("Unsuccessful");
        }

        public async Task<Response<string>> DeactivateNonAcademicStaffAsync(NonAcademicStaff staff)
        {
            var response =  _unitOfWork.NonAcademicStaff.UpdateNonAcademicStaff(staff);

            if(response)
            {
                 await _unitOfWork.SaveChangesAsync();
                 var responseString = $"{staff.AppUser.FirstName} {staff.AppUser.LastName} was deactivated";
                 return Response<string>.Success(null,responseString);
            }
            return Response<string>.Fail("Deactivation was unsuccessful");
        }
        public async Task<Response<string>> UpdateNonAcademicStaffAsync(NonAcademicStaff staff)
        {
           var result =  _unitOfWork.NonAcademicStaff.UpdateNonAcademicStaff(staff);
            if(result)
            {
                await _unitOfWork.SaveChangesAsync();
                var responseString = $"Update of {staff.AppUser.FirstName} {staff.AppUser.LastName} was successful";
                return Response<string>.Success(null, responseString);
            }
            return Response<string>.Fail("Update was unsuccessful");
        }
    }
}
