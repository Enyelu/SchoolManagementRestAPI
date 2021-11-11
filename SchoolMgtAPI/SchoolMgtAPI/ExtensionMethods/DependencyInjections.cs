using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Implementations;
using Services.Interfaces;
using Utilities.AppUnitOfWork;

namespace SchoolMgtAPI.ExtensionMethods
{
    public static class DependencyInjections
    {
        public static void  InjectServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<SchoolDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("default")));
            services.AddTransient<IMailService,MailService>();
            services.AddTransient<IAppUserService, AppUserService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAdminService,AdminService>();
            services.AddTransient<IClassAdviserService,ClassAdviserService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IFacultyService, FacultyService>();
            services.AddTransient<ILecturerService, LecturerService>();
            services.AddTransient<INonAcademicStaffService, NonAcademicStaffService>();
            services.AddTransient<IStudentService, StudentService>();
        }
    }
}
