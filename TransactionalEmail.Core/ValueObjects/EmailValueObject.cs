using HeyRed.MarkdownSharp;
using TransactionalEmail.Core.Constants;

namespace TransactionalEmail.Core.ValueObjects
{
    public class EmailValueObject
    {

        public EmailValueObject(string to, string subject, string message = "", string format = EmailFormat.TEXT)
        {
            To = to;
            Subject = subject;
            Message = message;
            Format = format;

            Initialize();
        }

        public string To { get; private set; }

        public string Subject { get; private set; }

        public string Message { get; private set; }

        public string Format { get; private set; }

        private string HtmlContent { get; set; }

        public string GetHtmlContent()
        {
            if (Format == EmailFormat.TEXT)
                return "";

            return Message;
        }

        private void Initialize()
        {
            Validate();

            if (Format == EmailFormat.MARKDOWN)
            {
                FormatMarkdown();
            }
        }
        private void FormatMarkdown()
        {
            var markdown = new Markdown();
            Message = markdown.Transform(Message);
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
