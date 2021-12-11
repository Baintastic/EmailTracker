using EmailTracker.Core;
using EmailTracker.Service.IServices;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmailTracker.Api.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelEmailController : Controller
    {
        private readonly ILabelEmailService labelEmailService;
        public LabelEmailController(ILabelEmailService labelFieldEmailService)
        {
            this.labelEmailService = labelFieldEmailService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLabel(LabelEmail labelledEmail)
        {
            await labelEmailService.AddLabelToEmail(labelledEmail);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLabel(int labelEmailId)
        {
            await labelEmailService.RemoveLabelFromEmail(labelEmailId);
            return Ok();
        }
    }
}
