using Mailjet.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;
using Service.Core.Interfaces;
using Service.Providers;

namespace Api.Configurations
{
    public static class MailProviderConfiguration
    {
        public static void ConfigureMailProviders(this IServiceCollection services, IConfigurationSection providerSettings)
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
