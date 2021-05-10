using System;

namespace TransactionalEmail.Consumer.Models
{
    public class User
    {
        public User()
        {
        }

        public User(string name, string email, string password)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ResetToken { get; set; }

        public DateTime? ResetTokenExpireDate { get; set; }
    }
}
