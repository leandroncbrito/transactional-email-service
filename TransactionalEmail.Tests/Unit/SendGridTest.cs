using TransactionalEmail.Core.DTO;
using TransactionalEmail.Core.Interfaces;
using Xunit;

namespace TransactionalEmail.Tests.Unit
{
    public class SendGridTest : ProviderTest
    {
        protected ISendGridProvider sendGridProvider;
        public SendGridTest(ISendGridProvider sendGridProvider) : base(sendGridProvider)
        {
            this.sendGridProvider = sendGridProvider;
        }

        [Fact]
        public async void SendEmailWithoutMessageIsNotPermitted()
        {
            var emailDTO = new EmailDTO(ToEmail, "Subject");

            var success = await sendGridProvider.SendEmailAsync(emailDTO);

            Assert.False(success);
        }
    }
}
