using TransactionalEmail.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using TransactionalEmail.Core.ValueObjects;
using TransactionalEmail.Core.Interfaces.Repositories;
using TransactionalEmail.Core.Interfaces.Services;
using System.Collections.Generic;

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

        public async void Store(EmailValueObject emailValueObject)
        {
            try
            {
                logger.LogInformation("Storing email sent");

                var emails = new List<Email>();
                foreach (var to in emailValueObject.Recipients)
                {
                    var email = new Email
                    {
                        Name = to.Name,
                        Address = to.Email,
                        Subject = emailValueObject.Subject,
                        Message = emailValueObject.Message,
                        Format = emailValueObject.Format
                    };

                    emails.Add(email);
                }

                await emailRepository.CreateManyAsync(emails);

                logger.LogInformation("Email successfully stored");
            }
            catch (Exception ex)
            {
                logger.LogInformation("Error storing email: {0}", ex.Message);
            }
        }
    }
}
