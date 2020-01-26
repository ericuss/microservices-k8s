namespace Lanre.Stock.Api.IntegrationTests
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Xunit.Abstractions;

    public class WebHostFixture : WebApplicationFactory<Program>
    {
        public ITestOutputHelper Output { get; set; }

        // Uses the generic host
        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = base.CreateHostBuilder();
            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders(); // Remove other loggers
                logging.AddXUnit(Output); // Use the ITestOutputHelper instance
            }).ConfigureWebHost(webHost =>
               {
                   // Add TestServer
                   webHost.UseTestServer();
                   webHost.UseStartup<Startup>();
                   //webHost.Configure(app => app.Run(async ctx =>
                   //    await ctx.Response.WriteAsync("Hello World!")));
               });

            ////// Build and start the IHost
            ////var host = await hostBuilder.StartAsync();

            ////// Create an HttpClient to send requests to the TestServer
            ////var client = host.GetTestClient();



            return builder;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices((services) =>
            {
                services.RemoveAll(typeof(IHostedService));
            });
        }
    }
}
