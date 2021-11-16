using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Mail;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Utilities.AppUnitOfWork;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Implementations
{
    public class NonAcademicStaffService : INonAcademicStaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        public NonAcademicStaffService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper, IConfiguration configuration, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _configuration = configuration;
            _mailService = mailService;
            _mapper = mapper;
        }

        public async Task<Response<string>> RegisterNonAcademicStaff(RegisterNonAcademicStaffDto NonAcademicStaffDto)
        {
            var appUser = _mapper.Map<AppUser>(NonAcademicStaffDto);
            var address = _mapper.Map<Address>(NonAcademicStaffDto);

            address.Id = Guid.NewGuid().ToString();
            await _unitOfWork.Address.AddAsync(address);
            await _unitOfWork.SaveChangesAsync();
            var readAddress = await _unitOfWork.Address.GetAddressAsync(address.Id);

            appUser.IsActive = true;
            appUser.DateCreated = DateTime.UtcNow.ToString();
            appUser.DateModified = appUser.DateCreated.ToString();
            appUser.Address = readAddress;

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var response = await _userManager.CreateAsync(appUser, NonAcademicStaffDto.Password);

                if (response.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, Roles.NonAcademicStaff);
                    var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    var encodedEmailToken = Encoding.UTF8.GetBytes(emailToken);
                    var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                    var callbackUrl = $"{_configuration["appURL"]}/api/Student/ConfirmStudentPassword?email={appUser.Email}&token={validEmailToken}";

                    var mail = new EmailRequest()
                    {
                        ToEmail = appUser.Email,
                        Subject = "Email Confirmation",
                        Attechments = null,
                        Body = $"<p> Dear {appUser.FirstName} \n Confirmation of your email is one click away <a href='{callbackUrl}'>click here</a> to continue</P>"
                    };

                    var result = await _mailService.SendMailAsync(mail);

                    if (result)
                    {
                        var department = await _unitOfWork.Department.GetDepartmentAsync(NonAcademicStaffDto.DepartmentName);
                        var faculty = await _unitOfWork.Faculty.GetFacultyAsync(NonAcademicStaffDto.FacultyName);
                        var position = await _unitOfWork.NonAcademicStaffPosition.GetNonAcademicStaffPositionAsync(NonAcademicStaffDto.Position);

                        var NonAcademicStaff = new NonAcademicStaff()
                        {
                            AppUser = appUser,
                            Department = department,
                            Faculty = faculty,
                            Position = position
                        };
                        await _unitOfWork.NonAcademicStaff.AddAsync(NonAcademicStaff);
                        await _unitOfWork.SaveChangesAsync();
                        transaction.Complete();
                        return Response<string>.Success(null, "Registration was successful");
                    }
                    return Response<string>.Fail("Email confirmation message not successful");
                }
                return Response<string>.Fail("Registration not successful. Retry...");
            }
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
                readStaff.Address.StreetNumber = staff.StreetNumber ;
                readStaff.Address.City = staff.City;
                readStaff.Address.State = staff.State;
                readStaff.Address.Country = staff.Country;

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
