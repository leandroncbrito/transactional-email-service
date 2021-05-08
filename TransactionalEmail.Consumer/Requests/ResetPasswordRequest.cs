using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TransactionalEmail.Consumer.Requests
{
    public class ResetPasswordRequest
    {
        [Required]
        [JsonPropertyNameAttribute("token")]
        public string Token { get; set; }

        [Required]
        [MinLength(6)]
        [JsonPropertyNameAttribute("password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [JsonPropertyNameAttribute("confirm_password")]
        public string ConfirmPassword { get; set; }
    }
}
