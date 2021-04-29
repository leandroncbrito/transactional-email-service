using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionalEmail.Core.DTO;

namespace TransactionalEmail.Infra.Ioc
{
    public static class MailSenderConfiguration
    {
        public static void ConfigureMailSender(this IServiceCollection services, IConfigurationSection senderSettings)
        {
            services.Configure<SenderSettings>(option =>
            {
                option.Name = senderSettings.GetValue<string>("Name");
                option.Mail = senderSettings.GetValue<string>("Mail");
            })
            .PostConfigure<SenderSettings>(options =>
            {
                if (string.IsNullOrEmpty(options.Mail))
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
