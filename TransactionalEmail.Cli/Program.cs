using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TransactionalEmail.Core.Interfaces;

namespace Cli
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = Startup.ConfigureServices();

            var emailService = serviceProvider.GetService<IEmailService>();

            // get from args Readline
            var to = "leandroncbrito@gmail.com";
            var subjet = "sending email from cli";
            var message = "console message";

            await emailService.SendEmail(to, subjet, message);
        }
    }
}
