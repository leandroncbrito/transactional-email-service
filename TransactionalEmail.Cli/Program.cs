using Microsoft.Extensions.DependencyInjection;

namespace Cli
{
    internal class Program
    {
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Build configuration
            // var configuration = new ConfigurationBuilder()
            //     .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            //     .AddJsonFile("appsettings.json", false)
            //     .Build();


        }
        public static void Main(string[] args)
        {
            // Execute(args).Wait();

            //var serviceProvider = new ServiceCollection()
            //    .AddScoped<IEmailService, EmailService>()
            //    .AddSingleton<ISendGridProvider, SendGridProvider>()
            //    .AddSingleton<IMailjetProvider, MailjetProvider>()
            //    .AddSingleton<IMailjetClient, MailjetClient>()
            //    .AddSingleton<ISendGridClient, SendGridClient>()

            //    //.AddSendGrid(options =>
            //    //{
            //    //    options.ApiKey = "";
            //    //})
            //    //.AddHttpClient<IMailjetClient, MailjetClient>(client =>
            //    //{
            //    //    client.UseBasicAuthentication("", "");
            //    //})
            //    .Configure<SenderSettings>(config =>
            //    {
            //        config.Mail = "leandroncbrito@live.com";
            //        config.Name = "Leandro Console";
            //    })
            //    .BuildServiceProvider();

            //var mailProviders = serviceProvider.GetServices<IMailjetProvider>();
            //var service = serviceProvider.GetService<IEmailService>();

            //var to = "leandroncbrito@gmail.com";
            //var subject = "Email test";
            //var message = "It's working";

            //await service.SendEmail(to, subject, message);
        }

        // static async Task Execute(string[] args)
        // {

        //     // var serviceProvider = new ServiceCollection()
        //     //     .AddSingleton<IEmailService, EmailService>()
        //     //     .AddSingleton<ISendGridProvider, SendGridProvider>()
        //     //     .BuildServiceProvider();

        //     // var to = "leandroncbrito@gmail.com";
        //     // var subject = "Email test";
        //     // var message = "It's working";

        //     // var emailService = serviceProvider.GetService<IEmailService>();

        //     // // var response = await emailService.SendEmail(to, subject, message);
        //     // await emailService.SendEmail(to, subject, message);

        //     //Console.WriteLine(response.Body);
        //     Console.WriteLine("Email sent");
    }
}
