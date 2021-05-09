using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TransactionalEmail.Core.DTO;
using TransactionalEmail.Core.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using TransactionalEmail.Core.Options;

namespace TransactionalEmail.Infra.Providers
{
    public class MailjetProvider : BaseProvider, IMailjetProvider
    {
        private readonly IMailjetClient client;

        public MailjetProvider(IMailjetClient client, IOptions<FromOptions> from, ILogger<MailjetProvider> logger) : base(from, logger)
        {
            this.client = client;
        }

        public override async Task<bool> SendEmailAsync(EmailDTO emailDTO)
        {
            Logger.LogInformation("Sending email using Mailjet provider");

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.SandboxMode, IsTesting)
            .Property(Send.FromEmail, FromOptions.Email)
            .Property(Send.FromName, FromOptions.Name)
            .Property(Send.Subject, emailDTO.Subject)
            .Property(Send.TextPart, emailDTO.Message)
            .Property(Send.HtmlPart, emailDTO.GetHtmlContent())
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
                Logger.LogError("Status: {0}, Message: {1}", response.StatusCode.ToString(), response.GetErrorInfo());
                return false;
            }

            Logger.LogInformation("Email sent using Mailjet provider");
            return true;
        }
    }
}
