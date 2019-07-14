using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            if (!SerialCommunication.SendOffCommand(zone)) { return "Error"; };

            return $"OK";
        }
    }
}