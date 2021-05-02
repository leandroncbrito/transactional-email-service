using System;
using Microsoft.Extensions.DependencyInjection;
using TransactionalEmail.Core.DTO;

namespace TransactionalEmail.Infra.Ioc.Config
{
    public static class SenderConfiguration
    {
        internal static void Configure(IServiceCollection services, Action<FromDTO> senderSettings)
        {
            services.Configure<FromDTO>(senderSettings)
                .PostConfigure<FromDTO>(options =>
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
