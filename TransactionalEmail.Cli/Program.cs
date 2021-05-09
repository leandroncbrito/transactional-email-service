using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TransactionalEmail.Core.DTO;
using TransactionalEmail.Core.Interfaces;
using TransactionalEmail.Core.Interfaces.Services;

namespace TransactionalEmail.Cli
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var host = Startup.CreateHostBuilder(args).Build();

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

            var emailService = serviceProvider.GetService<IEmailService>();

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
    }
}
