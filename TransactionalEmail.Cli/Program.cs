using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TransactionalEmail.Core.DTO;
using TransactionalEmail.Core.Interfaces;

namespace Cli
{
    internal class Program
    {
        private static readonly AutoResetEvent _waitHandle = new AutoResetEvent(false);

        static void Main()
        {
            // Handle Control+C or Control+Break
            Console.CancelKeyPress += (o, e) =>
            {
                Console.WriteLine("Exit");
                // Allow the main thread to continue and exit...
                _waitHandle.Set();
            };

            Execute().Wait();

            Console.WriteLine("Press CTRL + C to exit");

            // wait until Set method calls
            _waitHandle.WaitOne();
        }

        static async Task Execute()
        {
            Console.WriteLine("\nTRANSACTIONAL EMAIL SERVICE");

            // @TODO: add input validation
            Console.Write("\nTo: ");
            var to = Console.ReadLine();

            Console.Write("Subject: ");
            var subject = Console.ReadLine();

            Console.Write("Message: ");
            var message = Console.ReadLine();

            var serviceProvider = Startup.ConfigureServices();

            var emailService = serviceProvider.GetService<IEmailService>();

            Console.WriteLine("\nSending email...");

            var emailDTO = new EmailDTO(to, subject, message);

            var response = await emailService.SendEmailAsync(emailDTO);

            if (response)
            {
                Console.WriteLine("Email successfully sent");
            }

            Console.WriteLine("Error sending email");
        }
    }
}
