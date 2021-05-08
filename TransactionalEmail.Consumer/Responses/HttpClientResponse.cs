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

        public HttpClientResponse(HttpStatusCode? status, string title, bool success = false)
        {
            Status = status;
            Title = title;
            Success = success;
        }

        [JsonPropertyNameAttribute("status")]
        public HttpStatusCode? Status { get; set; }

        [JsonPropertyNameAttribute("title")]
        public string Title { get; set; }

        [JsonPropertyNameAttribute("success")]
        public bool Success { get; set; }

        // [JsonPropertyNameAttribute("errors")]
        // public IEnumerable<string> Errors { get; set; }
    }
}
