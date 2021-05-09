using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TransactionalEmail.Consumer.Responses
{
    public class HttpClientResponse
    {
        public HttpClientResponse()
        {
        }

        public HttpClientResponse(HttpStatusCode? status, string title)
        {
            Status = status;
            Title = title;
        }

        [JsonPropertyNameAttribute("status")]
        public HttpStatusCode? Status { get; set; }

        [JsonPropertyNameAttribute("title")]
        public string Title { get; set; }

        // [JsonPropertyNameAttribute("errors")]
        // public IEnumerable<string> Errors { get; set; }
    }
}
