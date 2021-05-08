using System.Text.Json.Serialization;

namespace TransactionalEmail.Consumer.Requests
{
    public class EmailClientRequest
    {
        public EmailClientRequest(string to, string subject, string message, string format = "html")
        {
            To = to;
            Subject = subject;
            Message = message;
            Format = format;
        }

        [JsonPropertyNameAttribute("to")]
        public string To { get; set; }

        [JsonPropertyNameAttribute("subject")]
        public string Subject { get; set; }

        [JsonPropertyNameAttribute("message")]
        public string Message { get; set; }

        [JsonPropertyNameAttribute("format")]
        public string Format { get; set; }
    }
}
