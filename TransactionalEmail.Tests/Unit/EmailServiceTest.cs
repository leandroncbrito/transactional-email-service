using System;
using Xunit;
using TransactionalEmail.Core.DTO;
using TransactionalEmail.Core.Constants;
using TransactionalEmail.Core.Interfaces.Services;

namespace TransactionalEmail.Tests.Unit
{
    public class EmailServiceTest : TestCase
    {
        private readonly IEmailService emailService;

        public EmailServiceTest(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [Fact]
        public async void SendEmailSuccessfully()
        {
            var emailDTO = new EmailDTO(ToEmail, "Subject", "Message");

            var success = await emailService.SendEmailAsync(emailDTO);

            Assert.True(success);
        }

        [Fact]
        public async void SendEmailWithHtmlContentSuccessfully()
        {
            var emailDTO = new EmailDTO(ToEmail, "Subject", "<strong>Message</strong>", EmailFormat.HTML);

            var success = await emailService.SendEmailAsync(emailDTO);

            Assert.True(success);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void SendEmailWithNullOrEmptyToThrowsArgumentNullException(string to)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new EmailDTO(to, "Subject", "Message");
            });
        }

        [Fact]
        public void SendEmailWithoutSubjectThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new EmailDTO(ToEmail, null, "Message");
            });
        }
    }
}
