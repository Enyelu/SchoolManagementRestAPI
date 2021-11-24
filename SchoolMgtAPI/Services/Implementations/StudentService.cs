using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Mail;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Utilities.AppUnitOfWork;
using Utilities.Dtos;
using Utilities.GeneralResponse;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IPasswordService _passwordService;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly string baseUrl;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IConfiguration configuration, 
                              IPasswordService passwordService, IMailService mailService, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mailService = mailService;
            _passwordService = passwordService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            baseUrl = env.IsDevelopment() ? _configuration["AppUrl"] : _configuration["HerokuUrl"];
        } 

        public async Task<Response<string>> RegisterStudentAsync(RegisterStudentDto student)
        {
            var appUser = _mapper.Map<AppUser>(student);
            var address = _mapper.Map<Address>(student);
            

            appUser.IsActive = true;
            appUser.DateCreated = DateTime.UtcNow.ToString();
            appUser.DateModified = appUser.DateCreated.ToString();
            appUser.Address = address;

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var response = await _userManager.CreateAsync(appUser, student.Password);

                if (response.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, Roles.Student);
                    var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    var encodedEmailToken = Encoding.UTF8.GetBytes(emailToken);
                    var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                    var callbackUrl = $"{baseUrl}/api/Student/ConfirmStudentEmail?email={appUser.Email}&token={validEmailToken}";

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
                        var department = await _unitOfWork.Department.GetDepartmentAsync(student.DepartmentName);
                        var faculty = await _unitOfWork.Faculty.GetFacultyAsync(student.FacultyName);
                        var classAdviser = await _unitOfWork.ClassAdviser.GetClassAdviserByEmailAsync(student.Email);

                        var newStudent = new Student()
                        {
                            RegistrationNumber = student.RegistrationNumber,
                            Class = student.Class,
                            Level = student.Level,
                            AppUser = appUser,
                            ClassAdviser = classAdviser,
                            Department = department,
                            Faculty = faculty,
                        };
                        appUser.Student = newStudent;
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

        public async Task<bool> ConfirmStudentEmailAsync(string email, string token)
        {
            var respose = await _passwordService.ConfirmEmailAsync(email, token);

            if (respose.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<Response<ReadStudentResponseDto>> ReadStudentAsync(string resgitrationNumber)
        {
            var response = await _unitOfWork.Student.GetStudentAsync(resgitrationNumber);
            var studentDetail = _mapper.Map<ReadStudentResponseDto>(response);

            if (studentDetail != null)
            {
                return Response<ReadStudentResponseDto>.Success(studentDetail, "Student Found");
            }
            return Response<ReadStudentResponseDto>.Fail("Student not found");
        }

        public async Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInALevelAsync(int studentsLevel)
        {
            var students = await _unitOfWork.Student.GetAllStudentsInALevelAsync(studentsLevel);
            var studentDetail = _mapper.Map<IEnumerable<ReadStudentResponseDto>>(students);

            return Response<IEnumerable<ReadStudentResponseDto>>.Success(studentDetail, "Successful");
        }

        public async Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInADepartmentInALevelAsync(int studentsLevel, string department)
        {
            var students = await _unitOfWork.Student.GetAllStudentsInADepartmentInALevelAsync(studentsLevel, department);

            if (students != null)
            {
                var studentsDetail = _mapper.Map<IEnumerable<ReadStudentResponseDto>>(students);
                return Response<IEnumerable<ReadStudentResponseDto>>.Success(studentsDetail, "Successful");
            }

            return Response<IEnumerable<ReadStudentResponseDto>>.Fail("An error occured. Try again");
        }

        public async Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInAFacultyInALevelAsync(int studentsLevel, string faculty)
        {
            var students = await _unitOfWork.Student.GetAllStudentsInAFacultyInALevelAsync(studentsLevel, faculty);

            if (students != null)
            {
                var studentsDetail = _mapper.Map<IEnumerable<ReadStudentResponseDto>>(students);
                return Response<IEnumerable<ReadStudentResponseDto>>.Success(studentsDetail, "Successful");
            }

            return Response<IEnumerable<ReadStudentResponseDto>>.Fail("An error occured. Try again");
        }

        public async Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInDepartmentAsync(string department)
        {
            var students = await _unitOfWork.Student.GetAllStudentsInDepartmentAsync(department);

            if (students != null)
            {
                var studentsDetail = _mapper.Map<IEnumerable<ReadStudentResponseDto>>(students);
                return Response<IEnumerable<ReadStudentResponseDto>>.Success(studentsDetail, "Successful");
            }

            return Response<IEnumerable<ReadStudentResponseDto>>.Fail("An error occured. Try again");
        }

        public async Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentsInFacultyAsync(string faculty)
        {
            var students = await _unitOfWork.Student.GetAllStudentsInFacultyAsync(faculty);

            if (students != null)
            {
                var studentsDetail = _mapper.Map<IEnumerable<ReadStudentResponseDto>>(students);
                return Response<IEnumerable<ReadStudentResponseDto>>.Success(studentsDetail, "Successful");
            }
            return Response<IEnumerable<ReadStudentResponseDto>>.Fail("An error occured. Try again");
        }

        public async Task<Response<IEnumerable<ReadStudentResponseDto>>> ReadAllStudentAsync()
        {
            var students = await _unitOfWork.Student.GetAllStudentsAsync();

            if (students != null)
            {
                var studentsDetail = _mapper.Map<IEnumerable<ReadStudentResponseDto>>(students);
                return Response<IEnumerable<ReadStudentResponseDto>>.Success(studentsDetail, "Successful");
            }
            return Response<IEnumerable<ReadStudentResponseDto>>.Fail("An error occured. Try again");
        }

        public async Task<Response<string>> DeactivateStudentAsync(string registrationNumber)
        {
            var student = await _unitOfWork.Student.GetStudentAsync(registrationNumber);

            if (student != null)
            {
                student.AppUser.IsActive = false;
                student.AppUser.DateModified = DateTime.UtcNow.ToString();
                _unitOfWork.Student.Update(student);
                await _unitOfWork.SaveChangesAsync();
                return Response<string>.Success(null, "Student deactivated successfully");
            }
            return Response<string>.Fail("Student not found");
        }
        public async Task<Response<string>> CheckStudentIsActiveAsync(string registrationNumber)
        {
            var student = await _unitOfWork.Student.GetStudentAsync(registrationNumber);

            if (student != null)
            {
                var studentStatus = student.AppUser.IsActive;
                if(studentStatus)
                {
                    return Response<string>.Success(null, "Student is active");
                }
            }
            return Response<string>.Fail("Student is not active or registration number is incorrect");
        }

        public async Task<Response<IEnumerable<string>>> RegisterCoursesAsync(string studentId, ICollection<string> courses)
        {
            var student = await _unitOfWork.Student.GetStudentAsync(null, studentId);

            if (student != null)
            {
                foreach (var course in courses)
                {
                    var searchedCourse = await _unitOfWork.Course.GetCourseByNameOrCourseCodeAsync(course);

                    if (!student.Courses.Contains(searchedCourse))
                    {
                        searchedCourse.Name = course;
                        student.Courses.Add(searchedCourse);
                        searchedCourse.Students.Add(student);
                        _unitOfWork.Student.Update(student);
                        await _unitOfWork.SaveChangesAsync();
                    }
                }
                var courseList = new List<string>();
                foreach (var course in student.Courses)
                {
                    courseList.Add(course.Name);
                }
                return Response<IEnumerable<string>>.Success(courseList, "Registration successful. These are your registered courses");
            }
            return Response<IEnumerable<string>>.Fail("An error occured while adding courses.", (int)HttpStatusCode.ExpectationFailed);
        }

        public async Task<Response<IEnumerable<string>>> RemoveCoursesAsync(string studentId, ICollection<string> courses)
        {

            var student = await _unitOfWork.Student.GetStudentAsync(null, studentId);
            var registeredCourses = student.Courses;

            if (student != null)
            {
                foreach (var course in courses)
                {
                    var searchedCourse = await _unitOfWork.Course.GetCourseByNameOrCourseCodeAsync(course);

                    if (registeredCourses.Contains(searchedCourse))
                    {
                        registeredCourses.Remove(searchedCourse);
                        _unitOfWork.Student.Update(student);
                    }
                }
                await _unitOfWork.SaveChangesAsync();
                return Response<IEnumerable<string>>.Success(courses, "Course(s) removal successful");
            }
            return Response<IEnumerable<string>>.Fail("An error occured while removing courses.", (int)HttpStatusCode.ExpectationFailed);
        }

        public async Task<Response<IEnumerable<string>>> ReadRegisteredCoursesAsync(string studentId)
        {
            var courses = await _unitOfWork.Student.GetRegisteredCoursesAsync(studentId);

            if (courses != null)
            {
                var courseList = new List<string>();
                foreach (var course in courses)
                {
                    courseList.Add(course.Name);
                }
                return Response<IEnumerable<string>>.Success(courseList, "Successful");
            }
            return Response<IEnumerable<string>>.Fail("No course has been registered");
        }
    }
}