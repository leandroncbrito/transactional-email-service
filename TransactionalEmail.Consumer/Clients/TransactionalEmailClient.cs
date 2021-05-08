using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using TransactionalEmail.Consumer.Responses;
using TransactionalEmail.Consumer.Requests;
using Microsoft.Extensions.Logging;

namespace TransactionalEmail.Consumer.Clients
{
    public class TransactionalEmailClient
    {
        public HttpClient client { get; }

        private readonly ILogger<TransactionalEmailClient> logger;

        public TransactionalEmailClient(HttpClient client, ILogger<TransactionalEmailClient> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public async Task<HttpClientResponse> SendEmailAsync(EmailClientRequest email)
        {
            logger.LogInformation("Sending email async");

            using var response = await client.PostAsJsonAsync("/email/send", email);

            // response.EnsureSuccessStatusCode();

            logger.LogInformation("Reading response json");
            return await response.Content.ReadFromJsonAsync<HttpClientResponse>();
        }
    }
}
