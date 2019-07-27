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
            if (! SerialCommunication.SendOnCommand(zone - 1)) { return "Error sending on command"; };

            if (! SerialCommunication.SendSelectSourceCommand(zone - 1, zone - 1)) { return "Error sending select source command"; };
            
            return $"OK";
        }
    }
}