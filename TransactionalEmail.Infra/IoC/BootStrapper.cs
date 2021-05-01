using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionalEmail.Core.Interfaces;
using TransactionalEmail.Core.Services;

namespace TransactionalEmail.Infra.Ioc
{
    public static class BootStrapper
    {
        public static void InitializeEmailServices(this IServiceCollection services, IConfigurationSection configuration)
        {
            AddEmailService(services);

            AddEmailConfiguration(services, configuration.GetSection("From"));

            AddEmailProviders(services, configuration.GetSection("Providers"));
        }

        private static void AddEmailService(IServiceCollection services)
        {
            services.AddSingleton<IEmailService, EmailService>();
        }

        private static void AddEmailConfiguration(IServiceCollection services, IConfigurationSection configuration)
        {
            SenderConfiguration.Configure(services, options =>
            {
                options.Email = configuration.GetValue<string>("Email");
                options.Name = configuration.GetValue<string>("Name");
            });
        }

        private static void AddEmailProviders(IServiceCollection services, IConfigurationSection configuration)
        {
            ProviderConfiguration.Configure(services, configuration);
        }
    }
}
