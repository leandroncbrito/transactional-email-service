using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionalEmail.Core.Options;
using TransactionalEmail.Infra.Ioc.Config;

namespace TransactionalEmail.Infra.Ioc
{
    public static class BootStrapper
    {
        public static void InitializeServices(this IServiceCollection services, IConfiguration configuration)
        {
            MailSettingsConfiguration.Configure(services, configuration.GetSection("MailSettings").Get<MailSettingsOptions>());

            ProviderConfiguration.Configure(services, configuration.GetSection("MailSettings:Providers"));

            ServiceConfiguration.AddServices(services, configuration);
        }
    }
}
