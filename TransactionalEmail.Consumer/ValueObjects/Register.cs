using System;

namespace TransactionalEmail.Consumer.ValueObjects
{
    public class Register
    {
        public Register(string email)
        {
            Email = email;
        }

        public string Email { get; private set; }

        public string Subject => "Registration completed";

        public string GetMessage()
        {
            return "Thank you for your registration.";
        }
    }
}
