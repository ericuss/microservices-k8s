using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureApi
    {
        public static IServiceCollection ConfigureApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            var httpsPort = configuration.GetValue<int>("https_port");

            return services
                .AddCustomHttps(httpsPort)
                .AddHttpContextAccessor()
                .AddMvc()
                .Services;
        }

        public static IApplicationBuilder UseApi(this IApplicationBuilder app, IWebHostEnvironment env, Action<IApplicationBuilder> useDocumentation, Action<IEndpointRouteBuilder> endpointsToAdd)
        {
            app
               .UseCustomHttps(env)
               .UseRouting()
               .UseAuthorization();

            useDocumentation(app);

            app.UseEndpoints(endpoints =>
            {
                endpointsToAdd(endpoints);
                endpoints.MapControllers();
            });

            return app;
        }
    }
}
