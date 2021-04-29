using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionalEmail.Core.Interfaces;

namespace TransactionalEmail.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEnumerable<IMailProvider> providers;

        public EmailService(IEnumerable<IMailProvider> providers)
        {
            this.providers = providers;
        }

        public async Task SendEmail(string to, string subject, string message)
        {
            foreach (var provider in providers)
            {
                await provider.SendEmailAsync(to, subject, message);
            }
        }
    }
}
