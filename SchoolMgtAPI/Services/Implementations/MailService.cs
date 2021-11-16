using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Models.Mail;
using Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class MailService : IMailService
    {
        private readonly EmailSettings _emailSettings;
        public MailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }
        public async Task<bool> SendMailAsync(EmailRequest emailRequest)
        {
            var mail = new MimeMessage();

            mail.Sender = MailboxAddress.Parse(_emailSettings.Mail);
            mail.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
            mail.Subject = emailRequest.Subject;

            var builder = new BodyBuilder();

            if(emailRequest.Attechments != null)
            {
                byte[] filtByte;

                foreach (var file in emailRequest.Attechments)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        filtByte = ms.ToArray();
                    }
                    builder.Attachments.Add(file.FileName, filtByte, ContentType.Parse(file.ContentType));
                }
            }
            
            builder.HtmlBody = emailRequest.Body;
            mail.Body = builder.ToMessageBody();

            try
            {
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_emailSettings.Mail, _emailSettings.Password);
                await smtp.SendAsync(mail);
                smtp.Disconnect(true);

                return true;
            }
            catch (ArgumentNullException e )
            {
                throw e;
            }
        }
    }
}