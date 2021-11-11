using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GeneralResponse;

namespace Services.Interfaces
{
    public interface IMailService
    {
        Task<Response<string>> SendMailAsync(string recipientEmail, string subject, string content);
    }
}
