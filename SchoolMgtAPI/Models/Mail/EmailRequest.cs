using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Models.Mail
{
    public class EmailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attechments {get;set;}
    }
}
