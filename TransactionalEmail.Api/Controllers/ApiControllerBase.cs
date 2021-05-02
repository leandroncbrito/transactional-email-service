using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionalEmail.Api.Responses;

namespace TransactionalEmail.Api.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        public OkObjectResult OkRequestResponse(string message)
        {
            var response = new EmailResponse()
            {
                Status = StatusCodes.Status200OK,
                Title = message
            };

            return Ok(response);
        }

        public BadRequestObjectResult BadRequestResponse(string message)
        {
            var response = new EmailResponse()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = message
            };

            return BadRequest(response);
        }
    }
}
