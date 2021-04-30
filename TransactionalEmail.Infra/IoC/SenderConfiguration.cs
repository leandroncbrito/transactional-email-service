using System;
using Microsoft.Extensions.DependencyInjection;
using TransactionalEmail.Core.DTO;

namespace TransactionalEmail.Infra.Ioc
{
    public static class SenderConfiguration
    {
        public static void Configure(IServiceCollection services, Action<From> senderSettings)
        {
            services.Configure<From>(senderSettings)
               .PostConfigure<From>(options =>
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
