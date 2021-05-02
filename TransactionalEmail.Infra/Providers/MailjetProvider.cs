using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TransactionalEmail.Core.DTO;
using TransactionalEmail.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace TransactionalEmail.Infra.Providers
{
    public class MailjetProvider : IMailProvider
    {
        private readonly IMailjetClient client;
        private readonly IOptions<From> fromEmail;
        private readonly ILogger<MailjetProvider> logger;

        public MailjetProvider(IMailjetClient client, IOptions<From> fromEmail, ILogger<MailjetProvider> logger)
        {
            this.client = client;
            this.fromEmail = fromEmail;
            this.logger = logger;
        }

        public async Task<bool> SendEmailAsync(string email, string subject, string message)
        {
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, fromEmail.Value.Email)
            .Property(Send.FromName, fromEmail.Value.Name)
            .Property(Send.Subject, subject)
            .Property(Send.TextPart, message)
            .Property(Send.HtmlPart, "<h3>Dear passenger, welcome to <a href=\"https://www.mailjet.com/\">Mailjet</a>!<br />May the delivery force be with you!")
            .Property(Send.Recipients, new JArray {
                new JObject {
                    {
                        "Email", email
                    }
                }
            });

            var response = await client.PostAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError(response.StatusCode, response.GetErrorInfo());
            }

            return response.IsSuccessStatusCode;
        }
    }
}
