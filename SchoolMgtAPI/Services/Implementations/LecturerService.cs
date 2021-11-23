using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Mail;
using Services.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Utilities.AppUnitOfWork;
using Utilities.Dtos;
using Utilities.GeneralResponse;

namespace Services.Implementations
{
    public class LecturerService : ILecturerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        public LecturerService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper, IConfiguration configuration, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _configuration = configuration;
            _mailService = mailService;
            _mapper = mapper;
        }

        public async Task<Response<string>> RegisterLecturerAsync(RegisterLecturerDto lecturerDto)
        {
            var appUser = _mapper.Map<AppUser>(lecturerDto);
            var address = _mapper.Map<Address>(lecturerDto);

            appUser.IsActive = true;
            appUser.DateCreated = DateTime.UtcNow.ToString();
            appUser.DateModified = appUser.DateCreated.ToString();
            appUser.Address = address;

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var response = await _userManager.CreateAsync(appUser, lecturerDto.Password);

                if (response.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, Roles.Lecturer);
                    var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    var encodedEmailToken = Encoding.UTF8.GetBytes(emailToken);
                    var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                    var callbackUrl = $"{_configuration["appURL"]}/api/Student/ConfirmStudentEmail?email={appUser.Email}&token={validEmailToken}";

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
                        var department = await _unitOfWork.Department.GetDepartmentAsync(lecturerDto.DepartmentName);
                        var faculty = await _unitOfWork.Faculty.GetFacultyAsync(lecturerDto.FacultyName);

                        var newLecturer = new Lecturer()
                        {
                            AppUser = appUser,
                            Department = department,
                            Faculty = faculty,
                        };
                       
                        appUser.Lecturer = newLecturer;
                        await _userManager.UpdateAsync(appUser);
                        await _unitOfWork.SaveChangesAsync();
                        transaction.Complete();
                        return Response<string>.Success(null, "Registration was successful");
                    }
                    return Response<string>.Fail("Email confirmation message not successful");
                }
                return Response<string>.Fail("Registration not successful. Retry...");
            }
        }

        public async Task<Response<LecturerResponseDto>> ReadLecturerDetailAsync(string lecturerEmail)
        {
            var lecturer = await _unitOfWork.Lecturer.GetLecturerDetailAsync(lecturerEmail);

            if (lecturer != null)
            {
                var mappedLecturer = _mapper.Map<LecturerResponseDto>(lecturer);
                return Response<LecturerResponseDto>.Success(mappedLecturer, "Successful");
            }
            return Response<LecturerResponseDto>.Fail("Unsuccessful. Lecturer not found");
        }

        public async Task<Response<string>> AssignCourseToLecturerAsync(string lecturerEmail, string courseName, string courseCode)
        {
            var lecturer = await _unitOfWork.Lecturer.GetLecturerDetailAsync(lecturerEmail);
            var course = await _unitOfWork.Course.GetCourseByNameOrCourseCodeAsync(courseCode, courseName);
            var responseCourse = courseName ?? courseCode;

            if (lecturer != null)
            {
                lecturer.Courses.Add(course);
                _unitOfWork.Lecturer.Update(lecturer);
                await _unitOfWork.SaveChangesAsync();
                
                return Response<string>.Success(null, $"Successfully assigned {responseCourse} to {lecturer.AppUser.Email}");
            }
            return Response<string>.Fail($"Unsuccessful. {responseCourse} was not added to {lecturer.AppUser.FirstName} with email: {lecturerEmail}.");
        }

        public async Task<Response<string>> DeactivateLecturerAsync(string lecturerEmail)
        {
            var lecturer = await _userManager.FindByEmailAsync(lecturerEmail);
            
            if(lecturer != null)
            {
                lecturer.IsActive = false;
                lecturer.DateModified = DateTime.UtcNow.ToString();
                var result = await _userManager.UpdateAsync(lecturer);

                if(result.Succeeded)
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
            var lecturer = await _unitOfWork.Lecturer.GetLecturerDetailAsync(lecturerEmail);

            if(lecturer != null)
            {
                lecturer.AppUser.FirstName = lecturerDto.FirstName;
                lecturer.AppUser.MiddleName = lecturerDto.MiddleName;
                lecturer.AppUser.LastName = lecturerDto.LastName;
                lecturer.AppUser.PhoneNumber = lecturerDto.PhoneNumber;
                lecturer.AppUser.Address.StreetNumber = lecturerDto.StreetNumber;
                lecturer.AppUser.Address.City = lecturerDto.City;
                lecturer.AppUser.Address.State = lecturerDto.State;
                lecturer.AppUser.Address.Country = lecturerDto.Country;
                lecturer.AppUser.DateModified = DateTime.UtcNow.ToString();

                _unitOfWork.Lecturer.Update(lecturer);
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(null, $"Updated successfully successful");
            }
            return Response<string>.Fail($"Udate was unsuccessful. {lecturerEmail} does not exist");
        }
    }
}
