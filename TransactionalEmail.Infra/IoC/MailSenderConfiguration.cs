using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransactionalEmail.Core.DTO;

namespace TransactionalEmail.Infra.Ioc
{
    public static class MailSenderConfiguration
    {
        public static void ConfigureMailSender(this IServiceCollection services, Action<SenderSettings> senderSettings)
        {
            services.Configure<SenderSettings>(senderSettings)
               .PostConfigure<SenderSettings>(options =>
               {
                   if (string.IsNullOrEmpty(options.Mail))
                   {
                       throw new Exception("Sender email is empty");
                   }

                   if (string.IsNullOrEmpty(options.Name))
                   {
                       throw new Exception("Sender name is empty");
                   }
               });
        }
    }
}
