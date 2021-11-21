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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Utilities.AppUnitOfWork;
using Utilities.Dtos;
using Utilities.GeneralResponse;

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

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, IConfiguration configuration, IPasswordService passwordService, IMailService mailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mailService = mailService;
            _passwordService = passwordService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 

        public async Task<Response<string>> RegisterStudentAsync(RegisterStudentDto student)
        {
            var appUser = _mapper.Map<AppUser>(student);
            var address = _mapper.Map<Address>(student);
            var student1 = _mapper.Map<Student>(student);

            appUser.IsActive = true;
            appUser.DateCreated = DateTime.UtcNow.ToString();
            appUser.DateModified = appUser.DateCreated.ToString();
            appUser.Address = address;
            appUser.Student = student1;

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var response = await _userManager.CreateAsync(appUser, student.Password);

                if (response.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, Roles.Student);
                    var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    var encodedEmailToken = Encoding.UTF8.GetBytes(emailToken);
                    var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                    var callbackUrl = $"{_configuration["AppUrl"]}/api/Student/ConfirmStudentEmail?email={appUser.Email}&token={validEmailToken}";

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

                        var newStudent = new Student()
                        {
                            AppUser = appUser,
                            Class = student.Class,
                            Department = department,
                            Faculty = faculty,
                        };
                        await _unitOfWork.Student.AddAsync(newStudent);
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
            List<ReadStudentResponseDto> studentsDetail = new List<ReadStudentResponseDto>();

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
            var studentStatus = student.AppUser.IsActive;

            if (studentStatus)
            {
                return Response<string>.Success(null, "Student is active");
            }

            return Response<string>.Fail("Student is not active");
        }

        public async Task<Response<IEnumerable<Course>>> RegisterCoursesAsync(string studentId, ICollection<string> courses)
        {
            var student = await _unitOfWork.Student.GetStudentAsync(null, studentId);

            if (student != null)
            {
                foreach (var course in courses)
                {
                    var searchedCourse = await _unitOfWork.Course.GetCourseByNameOrCourseCodeAsync(course);
                    searchedCourse.Name = course;
                    student.Courses.Add(searchedCourse);
                    searchedCourse.Students.Add(student);
                    _unitOfWork.Student.Update(student);
                    _unitOfWork.Course.Update(searchedCourse);
                }
                await _unitOfWork.SaveChangesAsync();
                var result = student.Courses.ToList();
                return Response<IEnumerable<Course>>.Success(result, "Registration successful");
            }
            return Response<IEnumerable<Course>>.Fail("An error occured while adding courses.", (int)HttpStatusCode.ExpectationFailed);
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
                        searchedCourse.Students.Remove(student);
                        _unitOfWork.Student.Update(student);
                        _unitOfWork.Course.Update(searchedCourse);
                    }
                }
                await _unitOfWork.SaveChangesAsync();
                return Response<IEnumerable<string>>.Success(courses, "Course(s) removal successful");
            }
            return Response<IEnumerable<string>>.Fail("An error occured while removing courses.", (int)HttpStatusCode.ExpectationFailed);
        }

        public async Task<Response<IEnumerable<Course>>> ReadRegisteredCoursesAsync(string studentId)
        {
            var courses = await _unitOfWork.Student.GetRegisteredCoursesAsync(studentId);

            if (courses != null)
            {
                return Response<IEnumerable<Course>>.Success(courses, "Successful");
            }
            return Response<IEnumerable<Course>>.Fail("No course has been registered");
        }
    }
}