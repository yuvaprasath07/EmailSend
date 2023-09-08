using EmailSend.Model;
using EmailSend.Service;
using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailSend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public SendMailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> MailSend([FromForm]EmailInfo source)
        {
            try
            {

                await _emailService.SendEmailAsync(source);
                return Ok("successfully Send");


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
