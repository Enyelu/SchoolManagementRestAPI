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
    public class LecturerService : ILecturerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LecturerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<Lecturer>> ReadLecturerDetailAsync(string lecturerId)
        {
           var lecturer = await _unitOfWork.Lecturer.GetLecturerDetailAsync(lecturerId);
            
            if (lecturer == null)
            {
                return Response<Lecturer>.Success(lecturer, "Successful");
            }
            return Response<Lecturer>.Fail("Unsuccessful. Lecturer not found");
        }

        public async Task<Response<string>> DeactivateLecturerAsync(string lecturerId)
        {
           var response =  await _unitOfWork.Lecturer.DeactivateLecturerAsync(lecturerId);
            
            if(response)
            {
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(null, "Deactivation was successful");
            }
            return Response<string>.Fail("Unsuccessful. Lecturer not found");
        }

        public async Task<Response<string>> UpdateLecturerAsync(Lecturer lecturer)
        {
            _unitOfWork.Lecturer.Update(lecturer);
            await _unitOfWork.SaveChangesAsync();
            return Response<string>.Success(null, $"{lecturer.AppUser.FirstName} {lecturer.AppUser.LastName} updated successfully");
        }
    }
}
