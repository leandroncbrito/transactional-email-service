using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionalEmail.Api.Responses;

namespace TransactionalEmail.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var response = new ApiResponse(StatusCodes.Status200OK, "API is up and running");

            return Ok(response);
        }
    }
}
