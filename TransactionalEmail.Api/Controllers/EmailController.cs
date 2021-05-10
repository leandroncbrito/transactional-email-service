using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransactionalEmail.Api.Requests;
using TransactionalEmail.Core.ValueObjects;
using TransactionalEmail.Core.Interfaces.Services;
using TransactionalEmail.Api.Responses;
using Microsoft.AspNetCore.Http;

namespace TransactionalEmail.Api.Controllers
{
    [Route("[controller]/[action]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailQueueService emailQueueService;
        private readonly ILogger<EmailController> logger;

        public EmailController(IEmailQueueService emailQueueService, ILogger<EmailController> logger)
        {
            this.emailQueueService = emailQueueService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] EmailRequest request)
        {
            logger.LogInformation("Email request received");

            var emailValueObject = new EmailValueObject(request.Recipients, request.Subject, request.Message, request.Format);

            await emailQueueService.Enqueue(emailValueObject);

            logger.LogInformation("Email added to the queue", emailValueObject);

            var response = new ApiResponse(StatusCodes.Status202Accepted, "Email added to the queue");

            return Accepted(response);
        }
    }
}
