using Microsoft.Extensions.DependencyInjection;
using TransactionalEmail.Core.Interfaces.Queue;
using TransactionalEmail.Core.Options;
using TransactionalEmail.Infra.Queue;

namespace TransactionalEmail.Infra.Ioc.Config
{
    public static class QueueSettingsConfiguration
    {
        internal static void Configure(IServiceCollection services, QueueSettingsOptions queueSettings)
        {
            services
                .AddHostedService<QueuedHostedService>()
                .AddSingleton<IBackgroundTaskQueue>(ctx =>
                {
                    return new BackgroundTaskQueue(queueSettings.Capacity);
                })
                .Configure<QueueSettingsOptions>(options =>
                {
                    options.Capacity = queueSettings.Capacity;
                    options.Attempts = queueSettings.Attempts;
                    options.SecondsInterval = queueSettings.SecondsInterval;
                });
        }
    }
}
