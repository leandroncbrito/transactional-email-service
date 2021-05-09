using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TransactionalEmail.Infra.Ioc;
using TransactionalEmail.Core.Interfaces;
using TransactionalEmail.Infra.Queue;
using TransactionalEmail.Core.Options;
using TransactionalEmail.Core.Interfaces.Queue;
using TransactionalEmail.Core.Services;
using TransactionalEmail.Core.Interfaces.Services;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });

            var queueSettings = Configuration.GetSection("QueueSettings").Get<QueueSettingsOptions>();

            services.AddHostedService<QueuedHostedService>()
                .AddSingleton<IBackgroundTaskQueue>(new BackgroundTaskQueue(queueSettings.Capacity))
                .Configure<QueueSettingsOptions>(options =>
                {
                    options.Capacity = queueSettings.Capacity;
                    options.Attempts = queueSettings.Attempts;
                    options.SecondsInterval = queueSettings.SecondsInterval;
                });

            services.AddSingleton<IEmailQueueService, EmailQueueService>();

            services.InitializeServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
