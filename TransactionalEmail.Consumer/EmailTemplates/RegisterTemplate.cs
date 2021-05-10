namespace TransactionalEmail.Consumer.EmailTemplates
{
    public class RegisterTemplate : BaseEmailTemplate
    {
        public override string Subject => "Registration completed";

        public override string GetMessage()
        {
            return "Thank you for your registration.";
        }
    }
}
