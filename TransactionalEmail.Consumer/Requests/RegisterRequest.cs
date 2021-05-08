using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TransactionalEmail.Consumer.Requests
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        [JsonPropertyNameAttribute("email")]
        public string Email { get; set; }
    }
}
