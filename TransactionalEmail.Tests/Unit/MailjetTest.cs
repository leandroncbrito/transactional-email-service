using TransactionalEmail.Core.DTO;
using TransactionalEmail.Core.Interfaces;
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
            var emailDTO = new EmailDTO(ToEmail, "Subject");

            var success = await mailjetProvider.SendEmailAsync(emailDTO);

            Assert.True(success);
        }
    }
}
