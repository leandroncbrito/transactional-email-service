using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TransactionalEmail.Consumer.Requests
{
    public class RegisterRequest
    {
        [Required]
        [JsonPropertyNameAttribute("name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [JsonPropertyNameAttribute("email")]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [JsonPropertyNameAttribute("password")]
        public string Password { get; set; }
    }
}
