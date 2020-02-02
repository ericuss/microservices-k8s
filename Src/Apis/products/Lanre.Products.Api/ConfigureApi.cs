using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureApi
    {
        public static IServiceCollection ConfigureApiServices(this IServiceCollection services)
        {
            return services
                .AddHttpContextAccessor()
                .AddMvc()
                .Services;
        }

        public static IApplicationBuilder UseApi(this IApplicationBuilder app, Action<IApplicationBuilder> useDocumentation, Action<IEndpointRouteBuilder> endpointsToAdd)
        {
            app
               .UseHttpsRedirection()
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
