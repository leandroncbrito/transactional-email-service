using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TransactionalEmail.Consumer.Clients;
using TransactionalEmail.Consumer.DTO;
using TransactionalEmail.Consumer.EmailTemplates;
using TransactionalEmail.Consumer.Interfaces.Repositories;
using TransactionalEmail.Consumer.Interfaces.Services;
using TransactionalEmail.Consumer.Models;
using TransactionalEmail.Consumer.Requests;
using TransactionalEmail.Consumer.Responses;
using TransactionalEmail.Consumer.ValueObjects;
using System.Net;
using System;

namespace TransactionalEmail.Consumer.Services
{
    public class AccountService : IAccountService
    {
        private readonly EmailClient emailClient;
        private readonly IUserRepository userRepository;

        private readonly ILogger<AccountService> logger;

        public AccountService(EmailClient emailClient, IUserRepository userRepository, ILogger<AccountService> logger)
        {
            this.emailClient = emailClient;
            this.userRepository = userRepository;
            this.logger = logger;
        }

        public async Task<HttpClientResponse> RegisterAsync(RegisterDTO register)
        {
            var user = new User(register.Name, register.Email, register.Password);

            var userCreated = await userRepository.Add(user);

            logger.LogDebug("User created:", userCreated);

            var newUserEmail = new EmailClientRequest(
                new To(user.Name, user.Email),
                new RegisterTemplate()
            );

            return await emailClient.SendEmailAsync(newUserEmail);
        }

        public async Task<HttpClientResponse> ForgotPasswordAsync(ForgotPasswordDTO forgotPassword)
        {
            var user = await userRepository.GetByEmail(forgotPassword.Email);

            if (user == null)
            {
                logger.LogInformation("User not found", forgotPassword.Email);
                return new HttpClientResponse(HttpStatusCode.NotFound, "User not found");
            }

            user.ResetToken = forgotPassword.Token;
            user.ResetTokenExpireDate = DateTime.UtcNow.AddDays(1);

            logger.LogDebug("Token and expire date generated");

            await userRepository.Update(user);

            logger.LogDebug("User updated");

            var forgotPasswordEmail = new EmailClientRequest(
                new To(user.Name, user.Email),
                new ForgotPasswordTemplate(forgotPassword.ValidateUrl)
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
