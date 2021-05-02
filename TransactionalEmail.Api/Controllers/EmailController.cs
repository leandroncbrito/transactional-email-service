using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransactionalEmail.Api.Requests;
using TransactionalEmail.Core.DTO;
using TransactionalEmail.Core.Interfaces;

namespace TransactionalEmail.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService emailService;

        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Send(EmailRequest request)
        {
            // validate request

            var emailDTO = new EmailDTO(request.To, request.Subject, request.Message);

            var success = await emailService.SendEmailAsync(emailDTO);

            if (!success)
            {
                return BadRequest("Error sending email");
            }

            return Ok("Email sent succesfully");
        }
    }
}
