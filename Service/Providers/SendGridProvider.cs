using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Service.Core.Interfaces;
using Service.Models;

namespace Service.Providers
{
    public class SendGridProvider : ISendGridProvider
    {
        private readonly IOptions<SenderSettings> senderSettings;
        private readonly ISendGridClient sendGridClient;

        public SendGridProvider(ISendGridClient sendGridClient, IOptions<SenderSettings> senderSettings)
        {
            this.sendGridClient = sendGridClient;
            this.senderSettings = senderSettings;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var htmlContent = "<strong>" + message + "</strong>";

            var from = new EmailAddress(senderSettings.Value.Mail, senderSettings.Value.Name);

            var tos = new List<EmailAddress>
            {
                new EmailAddress(email, "Temporary user name"),
            };

            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, message, htmlContent);

            var response = await sendGridClient.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                var responseBody = await response.Body.ReadAsStringAsync();

                throw new Exception("Fail to send email");
            }
        }
    }
}
