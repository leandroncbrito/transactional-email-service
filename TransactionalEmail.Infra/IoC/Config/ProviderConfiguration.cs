using Mailjet.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;
using TransactionalEmail.Core.Interfaces.Providers;
using TransactionalEmail.Infra.Providers;

namespace TransactionalEmail.Infra.Ioc.Config
{
    public static class ProviderConfiguration
    {
        internal static void Configure(IServiceCollection services, IConfigurationSection providerSettings)
        {
            services.AddSingleton<IMailProvider, SendGridProvider>();
            services.AddSingleton<ISendGridProvider, SendGridProvider>();

            services.AddSingleton<IMailProvider, MailjetProvider>();
            services.AddSingleton<IMailjetProvider, MailjetProvider>();

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
