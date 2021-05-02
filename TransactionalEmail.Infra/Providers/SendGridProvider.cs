using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using TransactionalEmail.Core.Interfaces;
using TransactionalEmail.Core.DTO;
using Microsoft.Extensions.Logging;

namespace TransactionalEmail.Infra.Providers
{
    public class SendGridProvider : IMailProvider
    {
        private readonly ISendGridClient client;
        private readonly IOptions<FromDTO> fromEmail;
        private readonly ILogger<SendGridProvider> logger;

        public SendGridProvider(ISendGridClient client, IOptions<FromDTO> fromEmail, ILogger<SendGridProvider> logger)
        {
            this.client = client;
            this.fromEmail = fromEmail;
            this.logger = logger;
        }

        public async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            logger.LogInformation("Sending email using Sendgrid provider");

            var htmlContent = "<strong>" + message + "</strong>";

            var from = new EmailAddress(fromEmail.Value.Email, fromEmail.Value.Name);

            var to = new EmailAddress(email, "Temporary user name");

            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, htmlContent);

            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.Body.ReadAsStringAsync();

                logger.LogError("Status: {0}, Message: {1}", response.StatusCode.ToString(), responseBody);
                return false;
            }

            logger.LogInformation("Email sent using Sendgrid provider");
            return true;
        }
    }
}
