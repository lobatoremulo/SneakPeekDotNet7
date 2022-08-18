using Microsoft.AspNetCore.Mvc;

namespace SneakPeekDotNet7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("Get")]
        public string Get()
        {
            return "String Qualquers";
        }

        [HttpGet]
        [Route("GetString")]
        public string GetString()
        {
            return "String Qualquer v2";
        }
    }
}
