namespace Lanre.Stock.Api.IntegrationTests
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Hosting;
    using System.Threading.Tasks;
    using Xunit;

    public class SampleWithHowtBuilder
    {
        [Fact]
        public async Task Test1()
        {
            var hostBuilder = new HostBuilder()
               .ConfigureWebHost(webHost =>
               {
                   // Add TestServer
                   webHost.UseTestServer();
                   webHost.Configure(app => app.Run(async ctx =>
                       await ctx.Response.WriteAsync("Hello World!")));
               });

            // Build and start the IHost
            var host = await hostBuilder.StartAsync();

            // Create an HttpClient to send requests to the TestServer
            var client = host.GetTestClient();

            var response = await client.GetAsync("/");

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Hello World!", responseString);
        }
    }
}
