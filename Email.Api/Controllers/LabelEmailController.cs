using EmailTracker.Core;
using EmailTracker.Service.IServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmailTracker.Api.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/email")]
    [ApiController]
    public class LabelEmailController : Controller
    {
        private readonly ILabelEmailService labelEmailService;
        public LabelEmailController(ILabelEmailService labelFieldEmailService)
        {
            this.labelEmailService = labelFieldEmailService;
        }

        [HttpPost("assign-label")]
        public async Task<IActionResult> AddLabelToEmail(LabelEmail labelledEmail)
        {
            await labelEmailService.AddLabelToEmail(labelledEmail);
            return Ok();
        }

        [HttpPost("unassign-label")]
        public async Task<IActionResult> RemoveLabelFromEmail(int labelEmailId)
        {
            await labelEmailService.RemoveLabelFromEmail(labelEmailId);
            return Ok();
        }
    }
}
