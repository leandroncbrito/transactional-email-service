
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using TransactionalEmail.Core.Interfaces.Queue;
using TransactionalEmail.Core.Interfaces.Repositories;
using TransactionalEmail.Core.Interfaces.Services;
using TransactionalEmail.Core.Services;
using TransactionalEmail.Infra.Repositories;

namespace TransactionalEmail.Infra.Ioc.Config
{
    public static class ServiceConfiguration
    {
        internal static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEmailQueueService, EmailQueueService>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IEmailLoggerService, EmailLoggerService>();
            services.AddSingleton<IEmailRepository, EmailRepository>();

            services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(configuration.GetConnectionString("MongoDb")));
        }
    }
}
