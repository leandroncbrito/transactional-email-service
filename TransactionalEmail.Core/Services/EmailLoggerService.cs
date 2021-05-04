using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionalEmail.Core.Interfaces;
using TransactionalEmail.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using TransactionalEmail.Core.DTO;

namespace TransactionalEmail.Core.Services
{
    public class EmailLoggerService : IEmailLoggerService
    {
        private readonly IEmailRepository emailRepository;
        private readonly ILogger<EmailLoggerService> logger;

        public EmailLoggerService(IEmailRepository emailRepository, ILogger<EmailLoggerService> logger)
        {
            this.emailRepository = emailRepository;
            this.logger = logger;
        }

        public async void Store(EmailDTO emailDTO)
        {
            try
            {
                logger.LogInformation("Storing email sent");

                var email = new Email(emailDTO.To, emailDTO.Subject, emailDTO.Message);

                var response = await emailRepository.CreateAsync(email);

                logger.LogInformation("Email {0} successfully stored at: {1}", response.ToString(), response.CreationTime);
            }
            catch (Exception ex)
            {
                logger.LogInformation("Error storing email: {0}", ex.Message);
            }
        }
    }
}
