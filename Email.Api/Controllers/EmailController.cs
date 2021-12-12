using EmailTracker.Core.Models;
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
        public async Task<IActionResult> SendEmail(Email email)
        {
            await emailService.SendEmail(email);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmail(int id)
        {
            await emailService.DeleteEmail(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UndeleteEmail(int id)
        {
            await emailService.UndeleteEmail(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmailById(int id)
        {
            var data = await emailService.GetEmailById(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmails()
        {
            var data = await emailService.GetAllEmails();
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterEmailsByLabelNameArchivedStatusOrFromEmailAddress(string labelName, bool? isArchived, string fromAddress )
        {
            var data = await emailService.FilterEmailsByLabelNameArchivedStatusOrFromEmailAddress(labelName, isArchived, fromAddress);
            if (data == null) return Ok();
            return Ok(data);
        }
    }
}
