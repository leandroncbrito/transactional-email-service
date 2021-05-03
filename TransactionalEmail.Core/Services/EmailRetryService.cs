using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TransactionalEmail.Core.DTO;
using TransactionalEmail.Core.Interfaces;
using TransactionalEmail.Core.Options;

namespace TransactionalEmail.Core.Services
{
    public class EmailRetryService : IEmailRetryDecorator
    {
        private readonly IEmailService emailService;
        private readonly ILogger<EmailRetryService> logger;
        private readonly int retriesCount = 3;

        public EmailRetryService(IEmailService emailService, IOptions<MailSettingsOptions> mailSettings, ILogger<EmailRetryService> logger)
        {
            this.emailService = emailService;
            this.logger = logger;
            this.retriesCount = mailSettings.Value.Retries;
        }

        public async Task<bool> SendEmailAsync(EmailDTO emailDTO)
        {
            try
            {
                var attempts = 1;

                while (attempts <= retriesCount)
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
