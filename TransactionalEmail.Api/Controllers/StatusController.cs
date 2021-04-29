using Microsoft.AspNetCore.Mvc;

namespace TransactionalEmail.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "API is up and running";
        }
    }
}
