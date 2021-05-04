using System;
using System.Threading;
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
        private readonly IOptions<RetryPolicyOptions> retrySettings;

        public EmailRetryService(IEmailService emailService, IOptions<RetryPolicyOptions> retrySettings, ILogger<EmailRetryService> logger)
        {
            this.emailService = emailService;
            this.logger = logger;
            this.retrySettings = retrySettings;
        }

        public async Task<bool> SendEmailAsync(EmailDTO emailDTO)
        {
            try
            {
                var attempts = 1;
                var retryMaxAttempts = retrySettings.Value.Attempts;
                var retryTime = TimeSpan.FromSeconds(retrySettings.Value.SecondsInterval);

                while (attempts <= retryMaxAttempts)
                {
                    if (attempts > 1)
                    {
                        logger.LogInformation($"Waiting {retryTime.TotalSeconds} seconds to try again");

                        Thread.Sleep((int)retryTime.TotalMilliseconds);
                    }

                    logger.LogInformation($"Attempt {attempts} to send email async");

                    var success = await emailService.SendEmailAsync(emailDTO);

                    if (success)
                    {
                        return true;
                    }

                    attempts++;
                }

                logger.LogInformation($"Failed to send email async, attempts: {retryMaxAttempts}");

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
