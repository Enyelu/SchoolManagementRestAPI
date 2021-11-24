using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Implementations;
using Services.Interfaces;
using Utilities.AppFluentValidation;
using Utilities.AppUnitOfWork;
using Utilities.Dtos;

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

            services.AddTransient<IValidator<CourseDto>, CourseValidator>();
            services.AddTransient<IValidator<CourseUpdateDto>, CourseUpdateValidator>();
            services.AddTransient<IValidator<LecturerUpdateDto>, LecturerUpdateValidator>();
            services.AddTransient<IValidator<NonAcademicStaffUpdateDto>, NonAcademicStaffUpdateValidator>();

            services.AddTransient <IValidator<RegisterLecturerDto>, RegisterLecturerValidator>();
            services.AddTransient<IValidator<RegisterNonAcademicStaffDto>, RegisterNonAcademicStaffValidator>();
            services.AddTransient <IValidator<RegisterStudentDto>, RegisterStudentValidator>();
            services.AddTransient <IValidator <UpdateAddressDto>, UpdateAddressValidator>();
        }
    }
}
