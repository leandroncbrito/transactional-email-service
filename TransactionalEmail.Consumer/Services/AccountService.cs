using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TransactionalEmail.Consumer.Clients;
using TransactionalEmail.Consumer.Requests;
using TransactionalEmail.Consumer.Responses;
using TransactionalEmail.Consumer.ValueObjects;

namespace TransactionalEmail.Consumer.Services
{
    public class AccountService : IAccountService
    {
        private readonly EmailClient emailClient;

        private readonly ILogger<AccountService> logger;

        public AccountService(EmailClient emailClient, ILogger<AccountService> logger)
        {
            this.emailClient = emailClient;
            this.logger = logger;
        }

        public async Task<HttpClientResponse> RegisterAsync(Register register)
        {
            var newUserEmail = new EmailClientRequest(
                register.Email,
                register.Subject,
                register.GetMessage()
            );

            return await emailClient.SendEmailAsync(newUserEmail);
        }

        public async Task<HttpClientResponse> ForgotPasswordAsync(ForgotPassword forgotPassword)
        {
            var forgotPasswordEmail = new EmailClientRequest(
                forgotPassword.Email,
                forgotPassword.Subject,
                forgotPassword.GetMessage()
            );

            return await emailClient.SendEmailAsync(forgotPasswordEmail);
        }

        public Task<HttpClientResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            return null;
            //return new HttpClientResponse(System.Net.HttpStatusCode.OK, "reset", true);
        }
    }
}
