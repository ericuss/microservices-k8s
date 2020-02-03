namespace Lanre.Products.Host
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.ApplicationInsights;

    public class Startup
    {
        public Startup(IHostEnvironment env)
        {
            Configuration = ConfigureBuilder.GetConfiguration(env);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var instrumentationKey = this.Configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");

            services
                .AddLogging(builder =>
                {
                    builder.AddApplicationInsights(instrumentationKey);
                    builder.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Information);
                })
                .AddApplicationInsightsTelemetry(x => x.InstrumentationKey = instrumentationKey)
                .ConfigureApiServices()
                .AddCustomHealthChecks()
                .AddCustomSwagger()
                ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .AddIf(env.IsDevelopment(), x => x.UseDeveloperExceptionPage())
                .UseApi(a => a.UseCustomSwagger(), endpoints => endpoints.UseHealthChecks())
                .UseWelcomePage()
                ;
        }
    }
}
