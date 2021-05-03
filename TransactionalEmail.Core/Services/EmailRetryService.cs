using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TransactionalEmail.Core.DTO;
using TransactionalEmail.Core.Interfaces;

namespace TransactionalEmail.Core.Services
{
    public class EmailRetryService : IEmailRetryDecorator
    {
        private readonly IEmailService emailService;
        private readonly ILogger<EmailRetryService> logger;
        private readonly int RetriesCount = 3;

        public EmailRetryService(IEmailService emailService, ILogger<EmailRetryService> logger)
        {
            this.emailService = emailService;
            this.logger = logger;
        }

        public async Task<bool> SendEmailAsync(EmailDTO emailDTO)
        {
            try
            {
                var attempts = 1;

                while (attempts <= RetriesCount)
                {
                    logger.LogInformation("Attempt {0} to send email async", attempts);

                    var success = await emailService.SendEmailAsync(emailDTO);

                    if (success)
                    {
                        return true;
                    }

                    attempts++;
                }

                logger.LogInformation($"Failed to send email async, attempts: {RetriesCount}");

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
