using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TransactionalEmail.Core.ValueObjects;
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

            var emailService = serviceProvider.GetService<IEmailQueueService>();

            var emailValueObject = new EmailValueObject(to, subject, message);

            Console.WriteLine("\nSending email...");

            await emailService.Enqueue(emailValueObject);

            Console.WriteLine("Email added to the queue");
        }
    }
}
