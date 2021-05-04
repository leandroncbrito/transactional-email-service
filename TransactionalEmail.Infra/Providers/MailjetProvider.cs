using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TransactionalEmail.Core.DTO;
using TransactionalEmail.Core.Interfaces;
using Microsoft.Extensions.Logging;
using TransactionalEmail.Core.Options;

namespace TransactionalEmail.Infra.Providers
{
    public class MailjetProvider : BaseProvider
    {
        private readonly IMailjetClient client;

        public MailjetProvider(IMailjetClient client, IOptions<FromOptions> fromEmail, ILogger<MailjetProvider> logger)
        : base(fromEmail, logger)
        {
            this.client = client;
        }

        public override async Task<bool> SendEmailAsync(EmailDTO emailDTO)
        {
            logger.LogInformation("Sending email using Mailjet provider");

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, fromEmail.Value.Email)
            .Property(Send.FromName, fromEmail.Value.Name)
            .Property(Send.Subject, emailDTO.Subject)
            .Property(Send.TextPart, emailDTO.Message)
            .Property(Send.SandboxMode, IsTesting)
            //.Property(Send.HtmlPart, "<h3>Dear passenger, welcome to <a href=\"https://www.mailjet.com/\">Mailjet</a>!<br />May the delivery force be with you!")
            .Property(Send.Recipients, new JArray {
                new JObject {
                    {
                        "Email", emailDTO.To
                    }
                }
            });

            var response = await client.PostAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                logger.LogError("Status: {0}, Message: {1}", response.StatusCode.ToString(), response.GetErrorInfo());
                return false;
            }

            logger.LogInformation("Email sent using Mailjet provider");
            return true;
        }
    }
}
