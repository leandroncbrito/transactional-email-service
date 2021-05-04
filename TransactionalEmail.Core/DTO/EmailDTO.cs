namespace TransactionalEmail.Core.DTO
{
    public class EmailDTO
    {
        public EmailDTO(string to, string subject, string message = "")
        {
            To = to;
            Subject = subject;
            Message = message;

            Validate();
        }

        public string To { get; private set; }

        public string Subject { get; private set; }

        public string Message { get; private set; }

        private void Validate()
        {
            if (string.IsNullOrEmpty(To))
            {
                throw new System.ArgumentNullException("To is null or empty");
            }

            if (string.IsNullOrEmpty(Subject))
            {
                throw new System.ArgumentNullException("Subject is null or empty");
            }
        }
    }
}
