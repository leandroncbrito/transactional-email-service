using TransactionalEmail.Core.ValueObjects;
using TransactionalEmail.Core.Interfaces.Providers;
using Xunit;

namespace TransactionalEmail.Tests.Unit
{
    public class MailjetTest : ProviderTest
    {
        protected readonly IMailjetProvider mailjetProvider;
        public MailjetTest(IMailjetProvider mailjetProvider) : base(mailjetProvider)
        {
            this.mailjetProvider = mailjetProvider;
        }

        [Fact]
        public async void SendEmailWithoutMessageIsPermitted()
        {
            var emailValueObject = new EmailValueObject(Recipients, "Subject");

            var success = await mailjetProvider.SendEmailAsync(emailValueObject);

            Assert.True(success);
        }
    }
}
