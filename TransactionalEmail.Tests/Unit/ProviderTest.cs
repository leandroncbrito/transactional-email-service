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
        public async void SendEmailSuccessfully()
        {
            var emailValueObject = new EmailValueObject(ToEmail, "Subject", "Message");

            var success = await emailProvider.SendEmailAsync(emailValueObject);

            Assert.True(success);
        }

        [Fact]
        public async void SendEmailWithHtmlContentSuccessfully()
        {
            var emailValueObject = new EmailValueObject(ToEmail, "Subject", "<strong>Message</strong>", EmailFormat.HTML);

            var success = await emailProvider.SendEmailAsync(emailValueObject);

            Assert.True(success);
        }
    }
}
