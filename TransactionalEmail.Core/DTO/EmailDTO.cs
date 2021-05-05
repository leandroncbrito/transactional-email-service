using TransactionalEmail.Core.Constants;

namespace TransactionalEmail.Core.DTO
{
    public class EmailDTO
    {

        public EmailDTO(string to, string subject, string message = "", string format = EmailFormat.TEXT)
        {
            To = to;
            Subject = subject;
            Message = message;
            Format = format;

            Validate();
        }

        public string To { get; private set; }

        public string Subject { get; private set; }

        public string Message { get; private set; }

        public string Format { get; private set; }

        public string GetHtmlContent()
        {
            return Format == EmailFormat.HTML
                ? Message
                : "";
        }

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
