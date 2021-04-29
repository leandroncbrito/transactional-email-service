using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using TransactionalEmail.Core.Interfaces;
using TransactionalEmail.Core.DTO;

namespace TransactionalEmail.Infra.Providers
{
    public class SendGridProvider : IMailProvider
    {
        private readonly ISendGridClient client;
        private readonly IOptions<SenderSettings> senderSettings;

        public SendGridProvider(ISendGridClient client, IOptions<SenderSettings> senderSettings)
        {
            this.client = client;
            this.senderSettings = senderSettings;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var htmlContent = "<strong>" + message + "</strong>";

            var from = new EmailAddress(senderSettings.Value.Mail, senderSettings.Value.Name);

            var to = new EmailAddress(email, "Temporary user name");

            var msg = MailHelper.CreateSingleEmail(from, to, subject + " Sendgrid", message, htmlContent);

            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.Body.ReadAsStringAsync();

                throw new Exception("Fail to send email: " + responseBody);
            }
        }
    }
}
