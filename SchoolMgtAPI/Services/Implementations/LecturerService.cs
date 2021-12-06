using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
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
        private readonly string baseUrl;
        public LecturerService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper, 
                               IConfiguration configuration, IMailService mailService, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _configuration = configuration;
            _mailService = mailService;
            _mapper = mapper;
            baseUrl = env.IsDevelopment() ? _configuration["AppUrl"] : _configuration["HerokuUrl"];
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

        public async Task<Response<LecturerResponseDto>> ReadLecturerDetailAsync(EmailRequestDto requestDto)
        {
            var lecturer = await _unitOfWork.Lecturer.GetLecturerDetailAsync(requestDto.Email);

            if (lecturer != null)
            {
                var mappedLecturer = _mapper.Map<LecturerResponseDto>(lecturer);
                return Response<LecturerResponseDto>.Success(mappedLecturer, "Successful");
            }
            return Response<LecturerResponseDto>.Fail("Unsuccessful. Lecturer not found");
        }

        public async Task<Response<string>> AssignCourseToLecturerAsync(AssignCourseDto CourseDto)
        {
            var lecturer = await _unitOfWork.Lecturer.GetLecturerDetailAsync(CourseDto.LecturerEmail);
            var course = await _unitOfWork.Course.GetCourseByNameOrCourseCodeAsync(CourseDto.CourseCode, CourseDto.CourseName);
            var responseCourse = CourseDto.CourseCode ?? CourseDto.CourseName;

            if (lecturer != null)
            {
                if(course != null)
                {
                    lecturer.Courses.Add(course);
                    _unitOfWork.Lecturer.Update(lecturer);
                    await _unitOfWork.SaveChangesAsync();
                    return Response<string>.Success(null, $"Successfully assigned {responseCourse} to {lecturer.AppUser.Email}");
                }
                return Response<string>.Success(null, $"{responseCourse} does not exist");
            }
            return Response<string>.Fail($"Unsuccessful. {responseCourse} was not added to {lecturer.AppUser.FirstName} with email: {CourseDto.LecturerEmail}.");
        }

        public async Task<Response<string>> UpdateLecturerAsync(LecturerUpdateDto lecturerDto, string lecturerEmail)
        {
            var lecturer = await _unitOfWork.Lecturer.GetLecturerDetailAsync(lecturerEmail);

            if (lecturer != null)
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