using System;
using Xunit;
using TransactionalEmail.Core.Interfaces.Providers;
using TransactionalEmail.Core.ValueObjects;
using TransactionalEmail.Core.Constants;

namespace TransactionalEmail.Tests.Unit
{
    public abstract class ProviderTest : TestCase
    {
        protected IMailProvider emailProvider;

        protected ProviderTest(IMailProvider emailProvider)
        {
            this.emailProvider = emailProvider;
        }

        [Fact]
        public async void SendEmailDefaultFormatSuccessfully()
        {
            var emailValueObject = new EmailValueObject(ToEmail, "Subject", "Message");

            var success = await emailProvider.SendEmailAsync(emailValueObject);

            Assert.True(success);
        }

        [Fact]
        public async void SendEmailWithTextFormatSuccessfully()
        {
            var emailValueObject = new EmailValueObject(ToEmail, "Subject", "Message text", EmailFormat.TEXT);

            var success = await emailProvider.SendEmailAsync(emailValueObject);

            Assert.True(success);
        }

        [Fact]
        public async void SendEmailWithHtmlFormatSuccessfully()
        {
            var emailValueObject = new EmailValueObject(ToEmail, "Subject", "<strong>Html strong</strong>", EmailFormat.HTML);

            var success = await emailProvider.SendEmailAsync(emailValueObject);

            Assert.True(success);
        }

        [Fact]
        public async void SendEmailWithMarkdownFormatSuccessfully()
        {
            var emailValueObject = new EmailValueObject(ToEmail, "Subject", "## Heading level 2", EmailFormat.MARKDOWN);

            var success = await emailProvider.SendEmailAsync(emailValueObject);

            Assert.True(success);
        }
    }
}
