using Microsoft.AspNetCore.Mvc;

namespace MZC_Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerOnController : ControllerBase
    {

        // GET api/poweron/1
        [HttpGet("{zone}")]
        public ActionResult<string> Get(int zone)
        {
            if (!SerialCommunication.SendOnCommand(zone)) { return "Error"; };

            if (!SerialCommunication.SendSelectSourceCommand(zone, zone)) { return "Error"; };
            
            return $"OK";
        }
    }
}