using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Mail;
using Services.Interfaces;
using System.Threading.Tasks;

namespace SchoolMgtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> SendMailAsync([FromForm]EmailRequest emailRequest)
        {
            try
            {
               var respons =  await _mailService.SendMailAsync(emailRequest);

                if(respons)
                {
                    return Ok();
                }
                return BadRequest(respons);
                
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
