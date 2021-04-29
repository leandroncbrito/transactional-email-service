using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Service.Core.Interfaces;
using Microsoft.Extensions.Options;
using Service.Models;

namespace Service.Providers
{
    public class MailjetProvider : IMailProvider
    {
        private readonly IMailjetClient client;
        private readonly IOptions<SenderSettings> senderSettings;

        public MailjetProvider(IMailjetClient client, IOptions<SenderSettings> senderSettings)
        {
            this.client = client;
            this.senderSettings = senderSettings;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, senderSettings.Value.Mail)
            .Property(Send.FromName, senderSettings.Value.Name)
            .Property(Send.Subject, subject + " Mailjet")
            .Property(Send.TextPart, message)
            .Property(Send.HtmlPart, "<h3>Dear passenger, welcome to <a href=\"https://www.mailjet.com/\">Mailjet</a>!<br />May the delivery force be with you!")
            .Property(Send.Recipients, new JArray {
                new JObject {
                    {
                        "Email", email
                    }
                }
            });

            MailjetResponse response = await client.PostAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Fail to send email: " + response.GetErrorInfo());
            }
        }
    }
}
