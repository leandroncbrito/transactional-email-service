namespace TransactionalEmail.Consumer.DTO
{
    public class ForgotPasswordDTO
    {
        public ForgotPasswordDTO(string email, string token, string validateUrl)
        {
            Email = email;
            Token = token;
            ValidateUrl = validateUrl;
        }

        public string Email { get; private set; }

        public string Token { get; private set; }

        public string ValidateUrl { get; private set; }
    }
}
