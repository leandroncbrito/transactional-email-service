using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TransactionalEmail.Core.DTO;
using TransactionalEmail.Core.Interfaces;
using TransactionalEmail.Infra.Ioc;

namespace TransactionalEmail.Cli
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                await Execute(serviceScope.ServiceProvider);
            }

            Console.WriteLine("Press any key to exit");

            Console.ReadKey();
        }

        static async Task Execute(IServiceProvider serviceProvider)
        {
            Console.WriteLine("\nTRANSACTIONAL EMAIL SERVICE");

            // @TODO: add input validation
            Console.Write("\nTo: ");
            var to = Console.ReadLine();

            Console.Write("Subject: ");
            var subject = Console.ReadLine();

            Console.Write("Message: ");
            var message = Console.ReadLine();

            var emailService = serviceProvider.GetService<IEmailRetryDecorator>();

            var emailDTO = new EmailDTO(to, subject, message);

            Console.WriteLine("\nSending email...");

            var success = await emailService.SendEmailAsync(emailDTO);

            if (!success)
            {
                Console.WriteLine("Error sending email");
                return;
            }

            Console.WriteLine("Email successfully sent");
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseEnvironment(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                .ConfigureHostConfiguration(config =>
                {
                    config.AddCommandLine(args);
                    config.AddEnvironmentVariables();
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((hostingContext, services) =>
                {
                    services.InitializeServices(hostingContext.Configuration);
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                });
    }
}
