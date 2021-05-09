using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransactionalEmail.Api.Requests;
using TransactionalEmail.Core.ValueObjects;
using TransactionalEmail.Core.Interfaces.Services;

namespace TransactionalEmail.Api.Controllers
{
    [Route("[controller]/[action]")]
    public class EmailController : ApiControllerBase
    {
        private readonly IEmailQueueService emailQueueService;
        private readonly ILogger<EmailController> logger;

        public EmailController(IEmailQueueService emailQueueService, ILogger<EmailController> logger)
        {
            this.emailQueueService = emailQueueService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Send(EmailRequest request)
        {
            logger.LogInformation("Email request received", request);

            var emailValueObject = new EmailValueObject(request.Recipients, request.Subject, request.Message, request.Format);

            await emailQueueService.Enqueue(emailValueObject);

            logger.LogInformation("Email added to the queue", emailValueObject);

            return AcceptedResponse("Email added to the queue");
        }
    }
}
