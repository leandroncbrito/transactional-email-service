using System;
using Xunit;
using TransactionalEmail.Core.ValueObjects;
using TransactionalEmail.Core.Constants;
using TransactionalEmail.Core.Interfaces.Services;
using TransactionalEmail.Core.DTO;
using System.Collections.Generic;
using TransactionalEmail.Core.Interfaces.Providers;
using Moq;
using TransactionalEmail.Core.Services;
using Microsoft.Extensions.Logging;

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
            var emailValueObject = new EmailValueObject(Recipients, "Subject", "Message");

            var success = await emailService.SendEmailAsync(emailValueObject);

            Assert.True(success);
        }

        [Fact]
        public async void SendEmailWithHtmlContentSuccessfully()
        {
            var emailValueObject = new EmailValueObject(Recipients, "Subject", "<strong>Message</strong>", EmailFormat.HTML);

            var success = await emailService.SendEmailAsync(emailValueObject);

            Assert.True(success);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "")]
        [InlineData("", "")]
        public void SendEmailWithNullOrEmptyToThrowsArgumentNullException(string name, string email)
        {
            var recipients = new List<To>() { new To(name, email) };

            Assert.Throws<ArgumentNullException>(() =>
            {
                new EmailValueObject(recipients, "Subject", "Message");
            });
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("abc@")]
        [InlineData("@abc")]
        [InlineData("abc.com")]
        public void SendEmailWithInvalidAddressThrowsValidationException(string email)
        {
            var recipients = new List<To>() { new To("", email) };

            Assert.Throws<ArgumentException>(() =>
            {
                new EmailValueObject(recipients, "Subject", "Message");
            });
        }

        [Fact]
        public void SendEmailWithoutSubjectThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new EmailValueObject(Recipients, null, "Message");
            });
        }

        [Fact]
        public async void SendEmailUsingFallbackProvider()
        {
            var emailValueObject = new EmailValueObject(Recipients, "Subject", "Message");

            var sendGridProviderMock = new Mock<ISendGridProvider>();
            var mailjetProviderMock = new Mock<IMailjetProvider>();

            var emailLoggerServiceMock = new Mock<IEmailLoggerService>();
            var loggerMock = new Mock<ILogger<EmailService>>();

            sendGridProviderMock.Setup(x => x.SendEmailAsync(emailValueObject)).ReturnsAsync(false);
            mailjetProviderMock.Setup(x => x.SendEmailAsync(emailValueObject)).ReturnsAsync(true);

            var providers = new List<IMailProvider>();
            providers.Add(sendGridProviderMock.Object);
            providers.Add(mailjetProviderMock.Object);

            var emailServiceMock = new EmailService(providers, emailLoggerServiceMock.Object, loggerMock.Object);
            var success = await emailServiceMock.SendEmailAsync(emailValueObject);

            Assert.True(success);

            sendGridProviderMock.Verify(x => x.SendEmailAsync(emailValueObject), Times.Once);
            mailjetProviderMock.Verify(x => x.SendEmailAsync(emailValueObject), Times.Once);
        }

        [Fact]
        public async void SendEmailSuccessfullyFallbackShouldNotBeCalled()
        {
            var emailValueObject = new EmailValueObject(Recipients, "Subject", "Message");

            var sendGridProviderMock = new Mock<ISendGridProvider>();
            var mailjetProviderMock = new Mock<IMailjetProvider>();

            var emailLoggerServiceMock = new Mock<IEmailLoggerService>();
            var loggerMock = new Mock<ILogger<EmailService>>();

            sendGridProviderMock.Setup(x => x.SendEmailAsync(emailValueObject)).ReturnsAsync(true);
            mailjetProviderMock.Setup(x => x.SendEmailAsync(emailValueObject)).ReturnsAsync(true);

            var providers = new List<IMailProvider>();
            providers.Add(sendGridProviderMock.Object);
            providers.Add(mailjetProviderMock.Object);

            var emailServiceMock = new EmailService(providers, emailLoggerServiceMock.Object, loggerMock.Object);
            var success = await emailServiceMock.SendEmailAsync(emailValueObject);

            Assert.True(success);

            sendGridProviderMock.Verify(x => x.SendEmailAsync(emailValueObject), Times.Once);
            mailjetProviderMock.Verify(x => x.SendEmailAsync(emailValueObject), Times.Never);
        }
    }
}
