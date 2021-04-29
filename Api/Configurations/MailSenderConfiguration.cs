using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Models;

namespace Api.Configurations
{
    public static class MailSenderConfiguration
    {
        public static void ConfigureMailSender(this IServiceCollection services, IConfigurationSection senderSettings)
        {
            services.Configure<SenderSettings>(senderSettings)
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
