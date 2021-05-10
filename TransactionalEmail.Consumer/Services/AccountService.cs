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
using BC = BCrypt.Net.BCrypt;

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
            var userDB = await userRepository.GetByEmail(register.Email);

            if (userDB != null)
            {
                logger.LogInformation("User already exists", register.Email);
                return new HttpClientResponse(HttpStatusCode.Conflict, "User alread exists");
            }

            var hash = BC.HashPassword(register.Password);
            var user = new User(register.Name, register.Email, hash);

            var userCreated = await userRepository.Add(user);

            logger.LogDebug("User created:", userCreated);

            var newUserEmail = new EmailClientRequest(
                new To(user.Name, user.Email),
                new RegisterTemplate()
            );

            await emailClient.SendEmailAsync(newUserEmail);

            return new HttpClientResponse(HttpStatusCode.OK, "User registered successfully");
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

            await emailClient.SendEmailAsync(forgotPasswordEmail);

            return new HttpClientResponse(HttpStatusCode.OK, "Reset token generated successfully");

        }

        public async Task<HttpClientResponse> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO)
        {
            var user = await userRepository.GetByToken(resetPasswordDTO.Token);

            if (user == null)
            {
                logger.LogInformation("User not found", resetPasswordDTO.Token);
                return new HttpClientResponse(HttpStatusCode.NotFound, "User not found");
            }

            if (user.ResetTokenExpireDate < DateTime.UtcNow)
            {
                logger.LogInformation("Token expired", resetPasswordDTO.Token);
                return new HttpClientResponse(HttpStatusCode.Unauthorized, "Token expired");
            }

            user.Password = BC.HashPassword(resetPasswordDTO.Password);
            user.ResetToken = null;
            user.ResetTokenExpireDate = null;

            await userRepository.Update(user);

            return new HttpClientResponse(HttpStatusCode.OK, "Password reseted successfully");
        }
    }
}
