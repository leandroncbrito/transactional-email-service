using System;
using Microsoft.Extensions.DependencyInjection;
using TransactionalEmail.Core.Options;

namespace TransactionalEmail.Infra.Ioc.Config
{
    public static class MailSettingsConfiguration
    {
        internal static void Configure(IServiceCollection services, MailSettingsOptions mailSettings)
        {
            services
                .Configure<RetryPolicyOptions>(options =>
                {
                    options.Attempts = mailSettings.RetryPolicy.Attempts;
                    options.SecondsInterval = mailSettings.RetryPolicy.SecondsInterval;
                })
                .Configure<FromOptions>(options =>
                {
                    options.Email = mailSettings.From.Email;
                    options.Name = mailSettings.From.Name;
                })
                .PostConfigure<FromOptions>(options =>
                {
                    if (string.IsNullOrEmpty(options.Email))
                    {
                        throw new Exception("From email is empty");
                    }

                    if (string.IsNullOrEmpty(options.Name))
                    {
                        throw new Exception("From name is empty");
                    }
                });
        }
    }
}
