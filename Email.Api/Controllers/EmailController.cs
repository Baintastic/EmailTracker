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
        public EmailController(IEmailService labelService)
        {
            this.emailService = labelService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailTracker.Core.Models.Email email)
        {
            await emailService.SendEmail(email);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmail(int labelId)
        {
            await emailService.DeleteEmail(labelId);
            return Ok();
        }
    }
}
