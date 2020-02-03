
namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Hosting;

    public static class HttpsExtensions
    {
        public static IServiceCollection AddCustomHttps(this IServiceCollection services, int httpsPort)
        {
            services
                .AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                    options.HttpsPort = httpsPort;
                })
                ;
            return services;
        }

        public static IApplicationBuilder UseCustomHttps(this IApplicationBuilder app, IHostEnvironment env)
        {
            return app
                    .AddIf(!env.IsDevelopment(), x => x.UseHsts())
                    .UseHttpsRedirection()
                ;
        }
    }
}