using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Services.Implementations;
using Services.Interfaces;
using Utilities.AppFluentValidation;
using Utilities.AppUnitOfWork;
using Utilities.Dtos;
using Utilities.Implementation.TokenGeneration;
using Utilities.Interface.TokenGeneration;

namespace SchoolMgtAPI.ExtensionMethods
{
    public static class DependencyInjections
    {
        public static void  InjectServices(this IServiceCollection services, IConfiguration Configuration)
        {   
            services.AddTransient<IPasswordService, PasswordService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAdminService,AdminService>();
            services.AddTransient<IClassAdviserService,ClassAdviserService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IFacultyService, FacultyService>();
            services.AddTransient<ILecturerService, LecturerService>();
            services.AddTransient<INonAcademicStaffService, NonAcademicStaffService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();

            services.AddTransient<IValidator<CourseDto>, CourseValidator>();
            services.AddTransient<IValidator<CourseUpdateDto>, CourseUpdateValidator>();
            services.AddTransient<IValidator<LecturerUpdateDto>, LecturerUpdateValidator>();
            services.AddTransient<IValidator<NonAcademicStaffUpdateDto>, NonAcademicStaffUpdateValidator>();
            services.AddTransient <IValidator<RegisterLecturerDto>, RegisterLecturerValidator>();
            services.AddTransient<IValidator<RegisterNonAcademicStaffDto>, RegisterNonAcademicStaffValidator>();
            services.AddTransient <IValidator<RegisterStudentDto>, RegisterStudentValidator>();
            services.AddTransient <IValidator <UpdateAddressDto>, UpdateAddressValidator>();
            services.AddTransient<IValidator<DepartmentRequestDto>, DepartmentRequestValidation>();
            services.AddTransient<IValidator<NameDto>, NameValidator>();
            services.AddTransient<IValidator<EmailRequestDto>, EmailRequestValidator>();
            services.AddTransient<IValidator<AddLecturerRequestDto>, AddLecturerRequestValidator>();
            services.AddTransient<IValidator<RegistrationNumberDto>, RegistrationNumberValidator>();
            services.AddTransient<IValidator<ReadFacultyStudentsDto>, ReadFacultyStudentsValidator>();
            services.AddTransient<IValidator<ReadDepartmentsStudentDto>, ReadDepartmentStudentsValidator>();
            services.AddTransient<IValidator<LevelDto>, LevelValidator>();
            services.AddTransient<IValidator<LoginDto>, LoginValidator>();
            services.AddTransient<IValidator<AssignCourseDto>, AssignCourseValidator>();
            services.AddTransient<IValidator<ReadClassAdviserDto>, ReadClassAdviserValidator>();
            services.AddTransient<IValidator<AssignClassAdviserDto>, AssignClassAdviserValidator>();
            services.AddTransient<IValidator<ConfirmEmailDto>, ConfirmEmailDtoValidator>();
            services.AddTransient<IValidator<RefreshTokenRequestDto>, RefreshTokenRequestDtoValidator>();
            services.AddTransient<IValidator<ResetPasswordModel>, ResetPasswordModelValidator>();

            services.AddTransient<IValidator<RefreshTokenRequestDto>, RefreshTokenRequestDtoValidator>();
        }
    }
}
