using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionalEmail.Api.Responses;

namespace TransactionalEmail.Api.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        public OkObjectResult OkRequestResponse(string message)
        {
            var response = new ApiResponse(StatusCodes.Status200OK, message);

            return Ok(response);
        }

        public BadRequestObjectResult BadRequestResponse(string message)
        {
            var response = new ApiResponse(StatusCodes.Status400BadRequest, message);

            return BadRequest(response);
        }
    }
}
