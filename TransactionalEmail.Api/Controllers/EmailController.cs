using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public EmailController(IEmailRetryDecorator emailService)
        {
            this.emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Send(EmailRequest request)
        {
            var emailDTO = new EmailDTO(request.To, request.Subject, request.Message);

            var success = await emailService.SendEmailAsync(emailDTO);

            if (!success)
            {
                return BadRequestResponse("Error sending email");
            }

            return OkRequestResponse("Email successfully sent");
        }
    }
}
