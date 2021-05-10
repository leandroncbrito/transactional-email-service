namespace TransactionalEmail.Consumer.EmailTemplates
{
    public abstract class BaseEmailTemplate
    {
        public abstract string Subject { get; }

        public abstract string GetMessage();
    }
}
