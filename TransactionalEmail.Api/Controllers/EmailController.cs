using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransactionalEmail.Api.Requests;
using TransactionalEmail.Core.DTO;
using TransactionalEmail.Core.Interfaces;

namespace TransactionalEmail.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmailController : ApiControllerBase
    {
        private readonly IEmailRetryDecorator emailService;
        private readonly ILogger<EmailController> logger;

        public EmailController(IEmailRetryDecorator emailService, ILogger<EmailController> logger)
        {
            this.emailService = emailService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Send(EmailRequest request)
        {
            logger.LogInformation("Email request received", request);

            var emailDTO = new EmailDTO(request.To, request.Subject, request.Message, request.Format);

            var success = await emailService.SendEmailAsync(emailDTO);

            if (!success)
            {
                logger.LogError("Error trying to send email", request);
                return BadResponse("Error trying to send email");
            }

            logger.LogInformation("Email successfully sent");
            return OkResponse("Email successfully sent");
        }
    }
}
