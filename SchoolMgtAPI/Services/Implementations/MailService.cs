using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GeneralResponse;

namespace Services.Implementations
{
    public class MailService : IMailService
    {
        public async Task<Response<string>> SendMailAsync(string recipientEmail, string subject, string content)
        {
            
            return Response<string>.Success(null, "success");
        }
    }
}
