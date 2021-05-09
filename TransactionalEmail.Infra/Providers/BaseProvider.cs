using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TransactionalEmail.Core.ValueObjects;
using TransactionalEmail.Core.Interfaces.Providers;
using TransactionalEmail.Core.Options;

namespace TransactionalEmail.Infra.Providers
{
    public abstract class BaseProvider : IMailProvider
    {
        protected readonly ILogger<IMailProvider> Logger;

        protected FromOptions FromOptions = new FromOptions();

        protected bool IsTesting => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Testing";

        public BaseProvider(IOptions<FromOptions> from, ILogger<IMailProvider> logger = null)
        {
            FromOptions.Email = from.Value.Email;
            FromOptions.Name = from.Value.Name;

            Logger = logger;

            if (IsTesting)
            {
                LogSandboxMode();
            }
        }

        private void LogSandboxMode()
        {
            if (Logger != null)
            {
                Logger.LogWarning("Using Sandbox Mode");
            }
        }

        public abstract Task<bool> SendEmailAsync(EmailValueObject emailValueObject);
    }
}
