using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionalEmail.Api.Responses;

namespace TransactionalEmail.Api.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        public OkObjectResult OkResponse(string message)
        {
            var response = new ApiResponse(StatusCodes.Status200OK, message, true);

            return Ok(response);
        }

        public BadRequestObjectResult BadResponse(string message)
        {
            var response = new ApiResponse(StatusCodes.Status400BadRequest, message, false);

            return BadRequest(response);
        }
    }
}
