using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TransactionalEmail.Consumer.Clients;
using TransactionalEmail.Consumer.Helpers;
using TransactionalEmail.Consumer.Requests;
using TransactionalEmail.Consumer.Responses;

namespace TransactionalEmail.Consumer.Services
{
    public class AccountService : IAccountService
    {
        private readonly EmailClient emailClient;

        private readonly ILogger<AccountService> logger;

        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountService(EmailClient emailClient, IHttpContextAccessor httpContextAccessor, ILogger<AccountService> logger)
        {
            this.emailClient = emailClient;
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        public async Task<HttpClientResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            var newUserEmail = new EmailClientRequest(
                registerRequest.Email,
                "Registration completed",
                "Thank you for your registration."
            );

            return await emailClient.SendEmailAsync(newUserEmail);
        }

        public async Task<HttpClientResponse> ForgotPasswordAsync(ForgotPasswordRequest forgotRequest)
        {
            var token = Token.Generate();

            var host = httpContextAccessor.HttpContext.Request.Host.Value;

            var url = $"http://{host}/account/validate-reset-token?token={token}";

            var forgotPasswordEmail = new EmailClientRequest(
                forgotRequest.Email,
                "Reset your password",
                $"Click on this <a href=\"{url}\">link</a> to set a new password"
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
