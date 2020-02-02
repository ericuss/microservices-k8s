namespace Lanre.BFFs.Web.Api
{
    using Lanre.Bffs.Web.Api.Infrastructure.Settings;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.ApplicationInsights;
    using System.Collections.Generic;

    public class Startup
    {
        public Startup(IHostEnvironment env)
        {
            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables()
                ;
            
            if (env.IsDevelopment())
            {
                configBuilder.AddUserSecrets<Startup>(optional: true);
            }

            Configuration = configBuilder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var instrumentationKey = this.Configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");
            services
                .Configure<List<ApiSettings>>(this.Configuration.GetSection("Apis"))
                .AddLogging(builder => {
                    builder.AddApplicationInsights(instrumentationKey);
                    builder.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Information);
                })
                .AddApplicationInsightsTelemetry()
                .AddControllers()
                .Services
                .AddCustomHealthChecks(this.Configuration)
                .AddCustomSwagger()
                .AddCustomCors()
                .RegisterServices()
                ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseCustomCors()
                .UseHttpsRedirection()
                .UseRouting()
                .UseCustomSwagger()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.UseHealthChecks();
                    endpoints.MapControllers();
                });
        }
    }
}
