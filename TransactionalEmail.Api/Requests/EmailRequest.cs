using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TransactionalEmail.Core.DTO;

namespace TransactionalEmail.Api.Requests
{
    public class EmailRequest
    {
        [Required]
        [JsonPropertyNameAttribute("recipients")]
        public IEnumerable<To> Recipients { get; set; }

        [Required]
        [JsonPropertyNameAttribute("subject")]
        public string Subject { get; set; }

        [Required]
        [JsonPropertyNameAttribute("message")]
        public string Message { get; set; }

        [JsonPropertyNameAttribute("format")]
        public string Format { get; set; }
    }
}

