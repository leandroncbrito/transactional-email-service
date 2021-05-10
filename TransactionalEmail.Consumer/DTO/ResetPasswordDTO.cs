using System;

namespace TransactionalEmail.Consumer.DTO
{
    public class ResetPasswordDTO
    {
        public ResetPasswordDTO(string token, string password)
        {
            Token = token;
            Password = password;
        }

        public string Token { get; private set; }

        public string Password { get; private set; }
    }
}
