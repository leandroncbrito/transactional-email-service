namespace TransactionalEmail.Core.DTO
{
    public class EmailDTO
    {
        public EmailDTO(string to, string subject, string message)
        {
            To = to;
            Subject = subject;
            Message = message;
        }

        public string To { get; private set; }

        public string Subject { get; private set; }

        public string Message { get; private set; }
    }
}
