using Microsoft.AspNetCore.Mvc;

namespace MZC_Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerOffController : ControllerBase
    {
        // GET api/poweron/1
        [HttpGet("{zone}")]
        public ActionResult<string> Get(int zone)
        {
            if (!SpeakerCraftAmplifier.SendOffCommand(zone - 1)) { return "Error"; };

            return $"OK";
        }
    }
}