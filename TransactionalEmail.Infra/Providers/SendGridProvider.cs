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
    public class SendGridProvider : BaseProvider, ISendGridProvider
    {
        private readonly ISendGridClient client;

        public SendGridProvider(ISendGridClient client, IOptions<FromOptions> from, ILogger<SendGridProvider> logger) : base(from, logger)
        {
            this.client = client;
        }

        public override async Task<bool> SendEmailAsync(EmailDTO emailDTO)
        {
            Logger.LogInformation("Sending email using Sendgrid provider");

            var from = new EmailAddress(FromOptions.Email, FromOptions.Name);

            var to = new EmailAddress(emailDTO.To);

            var sendGridMessagge = MailHelper.CreateSingleEmail(from, to, emailDTO.Subject, emailDTO.Message, emailDTO.GetHtmlContent());

            sendGridMessagge.SetSandBoxMode(IsTesting);

            var response = await client.SendEmailAsync(sendGridMessagge);

            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.Body.ReadAsStringAsync();

                Logger.LogError("Status: {0}, Message: {1}", response.StatusCode.ToString(), responseBody);
                return false;
            }

            Logger.LogInformation("Email sent using Sendgrid provider");
            return true;
        }
    }
}
