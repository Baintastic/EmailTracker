using EmailTracker.Service.IServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmailTrack.Api.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        private readonly IEmailService emailService;
        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailTracker.Core.Models.Email email)
        {
            await emailService.SendEmail(email);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmail(int emailId)
        {
            await emailService.DeleteEmail(emailId);
            return Ok();
        }

        [HttpGet("sender")]
        public async Task<IActionResult> GetAllEmailsBySenderEmailAddress(string emailAddress)
        {
            var data = await emailService.GetAllEmailsBySenderEmailAddress(emailAddress);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> UndeleteEmail(int emailId)
        {
            await emailService.UndeleteEmail(emailId);
            return Ok();
        }

        [HttpGet("archived")]
        public async Task<IActionResult> GetAllDeletedEmails()
        {
            var data = await emailService.GetAllDeletedEmails();
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpGet("label")]
        public async Task<IActionResult> GetAllEmailsByLabel(string labelName)
        {
            var data = await emailService.GetAllEmailsByLabel(labelName);
            if (data == null) return Ok();
            return Ok(data);
        }
    }
}
