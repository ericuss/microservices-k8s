namespace Lanre.Products.Host
{
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseKestrel()
                        .UseSerilog((builderContext, config) => CreateSerilogLogger(builderContext, config))
                        .UseStartup<Startup>();
                });

        private static void CreateSerilogLogger(WebHostBuilderContext builderContext, LoggerConfiguration config)
        {
            var configuration = ConfigureBuilder.GetConfiguration(builderContext.HostingEnvironment);
            var instrumentationKey = configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");
            config
                .ReadFrom.Configuration(configuration)
                .WriteTo.ApplicationInsights(instrumentationKey, TelemetryConverter.Events, Serilog.Events.LogEventLevel.Information)
                .WriteTo.ApplicationInsights(instrumentationKey, TelemetryConverter.Traces, Serilog.Events.LogEventLevel.Information)
                ;
        }
    }
}
