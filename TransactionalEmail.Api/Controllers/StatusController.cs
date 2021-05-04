using Microsoft.AspNetCore.Mvc;

namespace TransactionalEmail.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ApiControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return OkResponse("API is up and running");
        }
    }
}
