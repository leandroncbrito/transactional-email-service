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
            var emailDTO = new EmailDTO(request.To, request.Subject, request.Message, request.Format);

            var success = await emailService.SendEmailAsync(emailDTO);

            if (!success)
            {
                return BadResponse("Error sending email");
            }

            return OkResponse("Email successfully sent");
        }
    }
}
