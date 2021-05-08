using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using TransactionalEmail.Consumer.Responses;
using TransactionalEmail.Consumer.Requests;

namespace TransactionalEmail.Consumer.Clients
{
    public class TransactionalEmailClient
    {
        public HttpClient client { get; }

        public TransactionalEmailClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<HttpClientResponse> SendEmailAsync(EmailClientRequest email)
        {
            using var response = await client.PostAsJsonAsync("/email/send", email);

            // response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<HttpClientResponse>();
        }
    }
}
