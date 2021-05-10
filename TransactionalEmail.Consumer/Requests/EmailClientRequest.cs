using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TransactionalEmail.Consumer.EmailTemplates;

namespace TransactionalEmail.Consumer.Requests
{
    public class EmailClientRequest
    {
        public EmailClientRequest(To recipient, BaseEmailTemplate emailTemplate, string format = "html")
        {
            Recipients = new List<To>()
            {
                new To(recipient.Name, recipient.Email)
            };

            Subject = emailTemplate.Subject;
            Message = emailTemplate.GetMessage();
            Format = format;
        }

        [JsonPropertyNameAttribute("recipients")]
        public List<To> Recipients { get; set; }

        [JsonPropertyNameAttribute("subject")]
        public string Subject { get; set; }

        [JsonPropertyNameAttribute("message")]
        public string Message { get; set; }

        [JsonPropertyNameAttribute("format")]
        public string Format { get; set; }
    }

    public class To
    {
        public To(string name, string email)
        {
            Name = name;
            Email = email;
        }

        [JsonPropertyNameAttribute("name")]
        public string Name { get; set; }

        [JsonPropertyNameAttribute("email")]
        public string Email { get; set; }
    }
}
