using Mailjet.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;
using TransactionalEmail.Core.Interfaces;
using TransactionalEmail.Infra.Providers;

namespace TransactionalEmail.Infra.Ioc
{
    public static class ProviderConfiguration
    {
        public static void Configure(IServiceCollection services, IConfigurationSection providerSettings)
        {
            services.AddSingleton<IMailProvider, SendGridProvider>();
            services.AddSingleton<IMailProvider, MailjetProvider>();

            services.AddSendGrid(options =>
            {
                options.ApiKey = providerSettings.GetValue<string>("SENDGRID_API_KEY");
            });

            services.AddHttpClient<IMailjetClient, MailjetClient>(client =>
            {
                client.UseBasicAuthentication(
                    providerSettings.GetValue<string>("MJ_APIKEY_PUBLIC"),
                    providerSettings.GetValue<string>("MJ_APIKEY_PRIVATE")
                );
            });
        }
    }
}
