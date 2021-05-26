using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TransactionalEmail.Core.ValueObjects;
using TransactionalEmail.Core.Interfaces.Services;
using System.Collections.Generic;
using TransactionalEmail.Core.DTO;

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

            var emails = new List<To>();

            var done = "";
            while (done != "ok")
            {
                Console.Write("\nName: ");
                var name = Console.ReadLine();

                Console.Write("Address: ");
                var address = Console.ReadLine();

                emails.Add(new To(name, address));

                Console.Write("\n\nWrite ok to finalize the recipients: ");
                done = Console.ReadLine();
            }

            Console.Write("Subject: ");
            var subject = Console.ReadLine();

            Console.Write("Message: ");
            var message = Console.ReadLine();

            var emailService = serviceProvider.GetService<IEmailService>();

            var emailValueObject = new EmailValueObject(emails, subject, message);

            Console.WriteLine("\nSending email...");

            await emailService.SendEmailAsync(emailValueObject);

            Console.WriteLine("Email sent successfully");
        }
    }
}
