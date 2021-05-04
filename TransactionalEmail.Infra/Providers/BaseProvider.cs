using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TransactionalEmail.Core.DTO;
using TransactionalEmail.Core.Interfaces;
using TransactionalEmail.Core.Options;

namespace TransactionalEmail.Infra.Providers
{
    public abstract class BaseProvider : IMailProvider
    {
        protected readonly IOptions<FromOptions> fromEmail;

        protected readonly ILogger<IMailProvider> logger;

        protected bool IsTesting => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Testing";

        public BaseProvider(IOptions<FromOptions> fromEmail, ILogger<IMailProvider> logger)
        {
            this.fromEmail = fromEmail;
            this.logger = logger;

            if (IsTesting)
            {
                logger.LogWarning("Using Sandbox Mode");
            }
        }

        public abstract Task<bool> SendEmailAsync(EmailDTO emailDTO);
    }
}
