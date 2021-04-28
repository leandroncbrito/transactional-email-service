using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Core.Interfaces;
using Api.Models;

namespace Api.Controllers
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
        public async Task Send(MailRequest request)
        {
            await emailService.SendEmail(request.To, request.Subject, request.Message);
        }
    }
}
