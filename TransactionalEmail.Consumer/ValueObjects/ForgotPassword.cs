namespace TransactionalEmail.Consumer.ValueObjects
{
    public class ForgotPassword
    {
        public ForgotPassword(string email, string validateUrl)
        {
            Email = email;
            ValidateUrl = validateUrl;
        }

        public string Email { get; private set; }

        public string ValidateUrl { get; private set; }

        public string Subject => "Reset your password";

        public string GetMessage()
        {
            return $"Click on this <a href=\"{ValidateUrl}\">link</a> to set a new password";
        }
    }
}
