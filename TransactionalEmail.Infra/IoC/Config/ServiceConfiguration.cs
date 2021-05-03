
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using TransactionalEmail.Core.Interfaces;
using TransactionalEmail.Core.Services;
using TransactionalEmail.Infra.Data;

namespace TransactionalEmail.Infra.Ioc.Config
{
    public static class ServiceConfiguration
    {
        internal static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEmailRetryDecorator, EmailRetryService>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IEmailLoggerService, EmailLoggerService>();
            services.AddSingleton<IEmailRepository, EmailRepository>();

            services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(configuration.GetConnectionString("MongoDb")));
        }
    }
}
