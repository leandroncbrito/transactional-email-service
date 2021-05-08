using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TransactionalEmail.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ApiControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return OkResponse("API is up and running");
        }
    }
}
