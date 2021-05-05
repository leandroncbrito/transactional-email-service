using System.ComponentModel.DataAnnotations;

namespace TransactionalEmail.Api.Requests
{
    public class EmailRequest
    {
        // @TODO: change to List
        [Required]
        [EmailAddress]
        public string To { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        public string Format { get; set; }
    }
}
