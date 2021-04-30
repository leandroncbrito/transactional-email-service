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
        private readonly IOptions<From> fromEmail;

        public SendGridProvider(ISendGridClient client, IOptions<From> fromEmail)
        {
            this.client = client;
            this.fromEmail = fromEmail;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var htmlContent = "<strong>" + message + "</strong>";

            var from = new EmailAddress(fromEmail.Value.Email, fromEmail.Value.Name);

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
