using System;
using Xunit;
using TransactionalEmail.Core.Interfaces;
using TransactionalEmail.Core.DTO;
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
            var emailDTO = new EmailDTO(ToEmail, "Subject", "Message");

            var success = await emailProvider.SendEmailAsync(emailDTO);

            Assert.True(success);
        }

        [Fact]
        public async void SendEmailWithHtmlContentSuccessfully()
        {
            var emailDTO = new EmailDTO(ToEmail, "Subject", "<strong>Message</strong>", EmailFormat.HTML);

            var success = await emailProvider.SendEmailAsync(emailDTO);

            Assert.True(success);
        }
    }
}
