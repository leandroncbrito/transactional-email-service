using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TransactionalEmail.Infra.Ioc
{
    public static class BootStrapper
    {
        public static void AddMailProviders(this IServiceCollection services, IConfigurationSection configuration)
        {
            SenderConfiguration.Configure(services, options =>
            {
                options.Email = configuration.GetValue<string>("From:Email");
                options.Name = configuration.GetValue<string>("From:Name");
            });

            ProviderConfiguration.Configure(services, configuration.GetSection("Providers"));
        }
    }
}
