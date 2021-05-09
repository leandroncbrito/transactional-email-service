using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using TransactionalEmail.Consumer.Responses;
using TransactionalEmail.Consumer.Requests;
using Microsoft.Extensions.Logging;

namespace TransactionalEmail.Consumer.Clients
{
    public class EmailClient
    {
        public HttpClient client { get; }

        private readonly ILogger<EmailClient> logger;

        public EmailClient(HttpClient client, ILogger<EmailClient> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public async Task<HttpClientResponse> SendEmailAsync(EmailClientRequest email)
        {
            logger.LogInformation("Calling api to send email async");

            using var response = await client.PostAsJsonAsync("/email/send", email);

            response.EnsureSuccessStatusCode();

            logger.LogDebug("Reading response json");

            return await response.Content.ReadFromJsonAsync<HttpClientResponse>();
        }
    }
}
