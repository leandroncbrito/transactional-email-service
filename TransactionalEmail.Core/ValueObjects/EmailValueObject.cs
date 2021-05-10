using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HeyRed.MarkdownSharp;
using TransactionalEmail.Core.Constants;
using TransactionalEmail.Core.DTO;

namespace TransactionalEmail.Core.ValueObjects
{
    public class EmailValueObject
    {
        public EmailValueObject(IEnumerable<To> recipients, string subject, string message = "", string format = EmailFormat.TEXT)
        {
            Recipients = recipients;
            Subject = subject;
            Message = message;
            Format = format;

            Initialize();
        }

        public IEnumerable<To> Recipients { get; set; }

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
            var emailAttribute = new EmailAddressAttribute();

            if (Recipients.ToList().Count == 0)
            {
                throw new ArgumentNullException("Recipients is empty");
            }

            foreach (var recipient in Recipients)
            {
                if (string.IsNullOrEmpty(recipient.Email))
                {
                    throw new ArgumentNullException("Email is null or empty");
                }

                if (!emailAttribute.IsValid(recipient.Email))
                {
                    throw new ArgumentException();
                }
            }

            if (string.IsNullOrEmpty(Subject))
            {
                throw new ArgumentNullException("Subject is null or empty");
            }
        }
    }
}
