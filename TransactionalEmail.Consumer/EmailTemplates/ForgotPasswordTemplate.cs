using TransactionalEmail.Consumer.EmailTemplates;

namespace TransactionalEmail.Consumer.ValueObjects
{
    public class ForgotPasswordTemplate : BaseEmailTemplate
    {
        public ForgotPasswordTemplate(string validateUrl)
        {
            ValidateUrl = validateUrl;
        }

        public string ValidateUrl { get; private set; }

        public override string Subject => "Reset your password";

        public override string GetMessage()
        {
            return $"Click on this <a href=\"{ValidateUrl}\">link</a> to set a new password";
        }
    }
}
