﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
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
        private readonly string baseUrl;
        public NonAcademicStaffService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper, 
                                       IConfiguration configuration, IMailService mailService, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _configuration = configuration;
            _mailService = mailService;
            _mapper = mapper;
            baseUrl = env.IsDevelopment() ? _configuration["AppUrl"] : _configuration["HerokuUrl"];
        }

        public async Task<Response<string>> RegisterNonAcademicStaff(RegisterNonAcademicStaffDto NonAcademicStaffDto)
        {
            var appUser = _mapper.Map<AppUser>(NonAcademicStaffDto);
            var address = _mapper.Map<Address>(NonAcademicStaffDto);

            appUser.IsActive = true;
            appUser.DateCreated = DateTime.UtcNow.ToString();
            appUser.DateModified = appUser.DateCreated.ToString();
            appUser.Address = address;

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var response = await _userManager.CreateAsync(appUser, NonAcademicStaffDto.Password);

                if (response.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, Roles.NonAcademicStaff);
                    var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    var encodedEmailToken = Encoding.UTF8.GetBytes(emailToken);
                    var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                    var callbackUrl = $"{baseUrl}/api/Auth/Confirm-Email?email={appUser.Email}&token={validEmailToken}";

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
                        appUser.NonAcademicStaff = NonAcademicStaff;
                        await _unitOfWork.SaveChangesAsync();
                        transaction.Complete();
                        return Response<string>.Success(null, "Registration was successful");
                    }
                    return Response<string>.Fail("Email confirmation message not successful");
                }
                return Response<string>.Fail("Registration not successful. Retry...");
            }
        }

        public async Task<Response<NonAcademicStaffResponseDto>> ReadNonAcademicStaffAsync(EmailRequestDto requestDto)
        {
            var readStaff = await _unitOfWork.NonAcademicStaff.GetNonAcademicStaffAsync(requestDto.Email);
            var staff = _mapper.Map<NonAcademicStaffResponseDto>(readStaff);

            if (staff != null)
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
        public async Task<Response<string>> UpdateNonAcademicStaffAsync(NonAcademicStaffUpdateDto staff, string staffEmail)
        {
            if(staffEmail == string.Empty)
            {
                var readStaff = await _unitOfWork.NonAcademicStaff.GetNonAcademicStaffAsync(staffEmail);

                if (readStaff != null)
                {
                    readStaff.AppUser.FirstName = staff.FirstName;
                    readStaff.AppUser.MiddleName = staff.MiddleName;
                    readStaff.AppUser.LastName = staff.LastName;
                    readStaff.AppUser.Address.StreetNumber = staff.StreetNumber;
                    readStaff.AppUser.Address.City = staff.City;
                    readStaff.AppUser.Address.State = staff.State;
                    readStaff.AppUser.Address.Country = staff.Country;
                    readStaff.AppUser.DateModified = DateTime.UtcNow.ToString();

                    _unitOfWork.NonAcademicStaff.Update(readStaff);
                    await _unitOfWork.SaveChangesAsync();
                    return Response<string>.Success(null, $"Update was successful");
                }
                return Response<string>.Fail($"Update was unsuccessful. {staffEmail} does not exist");
            }
            return Response<string>.Fail("Field cannot be empty");
        }
    }
}
