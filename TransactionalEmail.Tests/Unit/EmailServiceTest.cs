using System;
using Xunit;
using TransactionalEmail.Core.ValueObjects;
using TransactionalEmail.Core.Constants;
using TransactionalEmail.Core.Interfaces.Services;
using TransactionalEmail.Core.DTO;
using System.Collections.Generic;

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
    }
}
