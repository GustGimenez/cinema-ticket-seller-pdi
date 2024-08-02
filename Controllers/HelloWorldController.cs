using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cinema_ticket_seller_pdi.Controllers
{
    [ApiController]
    [Route("hello-world")]
    [AllowAnonymous]
    public class HelloWorldController : ControllerBase
    {
        private readonly ILogger<HelloWorldController> _logger;

        public HelloWorldController(ILogger<HelloWorldController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public String Get()
        {
            return "Hello World!";
        }
    }
}
