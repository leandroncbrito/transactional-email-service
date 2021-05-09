using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionalEmail.Core.Interfaces.Providers;
using Microsoft.Extensions.Logging;
using System;
using TransactionalEmail.Core.ValueObjects;
using TransactionalEmail.Core.Interfaces.Services;

namespace TransactionalEmail.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEnumerable<IMailProvider> providers;
        private readonly IEmailLoggerService emailLoggerService;
        private readonly ILogger<EmailService> logger;

        public EmailService(IEnumerable<IMailProvider> providers, IEmailLoggerService emailLoggerService, ILogger<EmailService> logger)
        {
            this.providers = providers;
            this.emailLoggerService = emailLoggerService;
            this.logger = logger;
        }

        public async Task<bool> SendEmailAsync(EmailValueObject emailValueObject)
        {
            try
            {
                logger.LogInformation("Sending email async");

                foreach (var provider in providers)
                {
                    logger.LogInformation("Provider found", provider);

                    var success = await provider.SendEmailAsync(emailValueObject);

                    if (success)
                    {
                        emailLoggerService.Store(emailValueObject);

                        return true;
                    }
                }

                logger.LogInformation("Unable to send email, check the providers settings");

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
