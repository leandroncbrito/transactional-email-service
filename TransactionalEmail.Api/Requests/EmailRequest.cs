using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TransactionalEmail.Core.DTO;

namespace TransactionalEmail.Api.Requests
{
    public class EmailRequest
    {
        [Required]
        public IEnumerable<To> Recipients { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        public string Format { get; set; }
    }
}

