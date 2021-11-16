using Models.Mail;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IMailService
    {
        Task<bool> SendMailAsync(EmailRequest emailRequest);
    }
}
