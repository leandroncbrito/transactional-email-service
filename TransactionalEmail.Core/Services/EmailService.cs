using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionalEmail.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using TransactionalEmail.Core.DTO;

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

        public async Task<bool> SendEmailAsync(EmailDTO emailDTO)
        {
            try
            {
                foreach (var provider in providers)
                {
                    logger.LogInformation("Provider found, sending email async");

                    var success = await provider.SendEmailAsync(emailDTO);

                    if (success)
                    {
                        await emailLoggerService.Store(emailDTO);

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
