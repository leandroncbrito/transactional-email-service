using Service.Core.Interfaces;
using System.Threading.Tasks;

namespace Service
{
    public class EmailService : IEmailService
    {
        private readonly ISendGridProvider sendGridProvider;

        public EmailService(ISendGridProvider sendGridProvider)
        {
            this.sendGridProvider = sendGridProvider;
        }

        public async Task SendEmail(string to, string subject, string message)
        {
            await sendGridProvider.SendEmailAsync(to, subject, message);
        }
    }
}
