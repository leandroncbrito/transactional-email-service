using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionalEmail.Core.DTO;
using TransactionalEmail.Core.Interfaces;
using TransactionalEmail.Core.Services;
using TransactionalEmail.Infra.Ioc.Config;
using TransactionalEmail.Infra.Data;
using MongoDB.Driver;

namespace TransactionalEmail.Infra.Ioc
{
    public static class BootStrapper
    {
        public static void InitializeServices(this IServiceCollection services, IConfiguration configuration)
        {
            var from = configuration.GetSection("MailSettings:From").Get<From>();

            SenderConfiguration.Configure(services, options =>
            {
                options.Email = from.Email;
                options.Name = from.Name;
            });

            ProviderConfiguration.Configure(services, configuration.GetSection("MailSettings:Providers"));

            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IEmailRepository, EmailRepository>();

            services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(configuration.GetConnectionString("MongoDb")));
        }
    }
}
