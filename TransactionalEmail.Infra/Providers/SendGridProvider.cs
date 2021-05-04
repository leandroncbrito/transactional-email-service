using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using TransactionalEmail.Core.Interfaces;
using TransactionalEmail.Core.DTO;
using Microsoft.Extensions.Logging;
using TransactionalEmail.Core.Options;

namespace TransactionalEmail.Infra.Providers
{
    public class SendGridProvider : BaseProvider
    {
        private readonly ISendGridClient client;

        public SendGridProvider(ISendGridClient client, IOptions<FromOptions> fromEmail, ILogger<SendGridProvider> logger)
        : base(fromEmail, logger)
        {
            this.client = client;
        }

        public override async Task<bool> SendEmailAsync(EmailDTO emailDTO)
        {
            logger.LogInformation("Sending email using Sendgrid provider");

            var htmlContent = "<strong>" + emailDTO.Message + "</strong>";

            var from = new EmailAddress(fromEmail.Value.Email, fromEmail.Value.Name);

            var to = new EmailAddress(emailDTO.To, "Temporary user name");

            var msg = MailHelper.CreateSingleEmail(from, to, emailDTO.Subject, emailDTO.Message, htmlContent);

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
