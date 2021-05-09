using System.ComponentModel.DataAnnotations;

namespace TransactionalEmail.Core.DTO
{
    public class To
    {
        public To(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; private set; }

        [Required]
        [EmailAddress]
        public string Email { get; private set; }
    }
}
