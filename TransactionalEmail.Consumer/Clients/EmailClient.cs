using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using TransactionalEmail.Consumer.Responses;
using TransactionalEmail.Consumer.Requests;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

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
            try
            {
                logger.LogInformation("Calling api to send email async", email);
                using var response = await client.PostAsJsonAsync("/email/send", email);

                logger.LogDebug("Reading json response");
                return await response.Content.ReadFromJsonAsync<HttpClientResponse>();
            }
            catch (HttpRequestException ex)
            {
                logger.LogCritical(ex.Message, ex.InnerException);
                return new HttpClientResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
            catch (JsonException ex)
            {
                logger.LogCritical(ex.Message, ex.InnerException);
                return new HttpClientResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
