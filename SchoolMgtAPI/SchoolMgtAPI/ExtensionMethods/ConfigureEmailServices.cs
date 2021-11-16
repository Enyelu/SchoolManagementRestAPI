using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.Mail;
using Services.Implementations;
using Services.Interfaces;

namespace SchoolMgtAPI.ExtensionMethods
{
    public static class ConfigureEmailServices
    {
        public static void ConfigureEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailService>();
        }
    }
}
