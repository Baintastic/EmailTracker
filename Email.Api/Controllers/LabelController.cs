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
    public class LabelController : Controller
    {
        private readonly ILabelService labelService;
        public LabelController(ILabelService labelService)
        {
            this.labelService = labelService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLabel(Label label)
        {
            await labelService.CreateLabel(label);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLabel(int id)
        {
            await labelService.DeleteLabel(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLabelById(int id)
        {
            var data = await labelService.GetLabelById(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLabels()
        {
            var data = await labelService.GetAllLabels();
            if (data == null) return Ok();
            return Ok(data);
        }
    }
}
