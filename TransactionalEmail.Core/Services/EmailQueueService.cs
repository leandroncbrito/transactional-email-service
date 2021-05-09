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
    public class EmailQueueService : IEmailQueueService
    {
        private EmailDTO Email { get; set; }
        private readonly IBackgroundTaskQueue taskQueue;
        private readonly IEmailService emailService;
        private readonly IOptions<RetryPolicyOptions> retrySettings;
        private readonly ILogger<EmailQueueService> logger;

        public EmailQueueService(IBackgroundTaskQueue taskQueue,
            IEmailService emailService,
            IOptions<RetryPolicyOptions> retrySettings,
            ILogger<EmailQueueService> logger)
        {
            this.taskQueue = taskQueue;
            this.emailService = emailService;
            this.retrySettings = retrySettings;
            this.logger = logger;
        }

        public async ValueTask Enqueue(EmailDTO email)
        {
            Email = email;

            await taskQueue.QueueBackgroundWorkItemAsync(BuildWorkItem);
        }

        private async ValueTask BuildWorkItem(CancellationToken token)
        {
            var attempts = 1;
            var retryMaxAttempts = retrySettings.Value.Attempts;
            var retryTime = TimeSpan.FromSeconds(retrySettings.Value.SecondsInterval);

            while (!token.IsCancellationRequested && attempts <= retryMaxAttempts)
            {
                try
                {
                    logger.LogInformation($"Attempt {attempts} to send email async");

                    var success = await emailService.SendEmailAsync(Email);

                    if (success)
                    {
                        logger.LogInformation("Queued Background Task is complete.");
                        break;
                    }

                    logger.LogInformation($"Waiting {retryTime.TotalSeconds} seconds to try again");
                    await Task.Delay(TimeSpan.FromSeconds(retryTime.TotalSeconds), token);
                }
                catch (System.Exception ex)
                {
                    logger.LogCritical(ex, "An unhandled exception occured");
                    throw;
                }

                if (attempts == retryMaxAttempts)
                {
                    logger.LogError($"Failed to send email async, attempts: {retryMaxAttempts}");
                }

                attempts++;
            }
        }
    }
}
