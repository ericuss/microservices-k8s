using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureApi
    {
        public static IServiceCollection ConfigureApiServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {
            var httpsPort = configuration.GetValue<int>("https_port");

            return services
                .AddCustomHttps(httpsPort, env)
                .AddHttpContextAccessor()
                .AddMvc()
                .Services;
        }

        public static IApplicationBuilder UseApi(this IApplicationBuilder app, IWebHostEnvironment env, Action<IApplicationBuilder> useDocumentation, Action<IEndpointRouteBuilder> endpointsToAdd)
        {
            app.UseCustomHttps(env);

            useDocumentation(app);

            app
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpointsToAdd(endpoints);
                    endpoints.MapControllers();
                });

            return app;
        }
    }
}
