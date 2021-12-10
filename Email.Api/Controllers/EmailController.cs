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

        [HttpGet]
        public async Task<IActionResult> GetAllEmailsBySenderEmailAddress(string senderEmailAddress)
        {
            var data = await emailService.GetAllEmailsBySenderEmailAddress(senderEmailAddress);
            if (data == null) return Ok();
            return Ok(data);
        }
    }
}
