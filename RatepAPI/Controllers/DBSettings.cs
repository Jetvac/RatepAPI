#nullable disable
using Microsoft.AspNetCore.Mvc;
using RatepAPI.Models;

namespace RatepAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DBSettings : ControllerBase
    {
        [HttpPost("ChangeConnectString")]
        public ActionResult<string> ChangeConnectString(string ServerName, string DBName)
        {
            VeloRaContext DBContext = new VeloRaContext();
            VeloRaContext.ConnectString = $"Server={ServerName};Database={DBName};Trusted_Connection=True;";
            DBContext = new VeloRaContext();
            return Ok();
        }
    }
}
