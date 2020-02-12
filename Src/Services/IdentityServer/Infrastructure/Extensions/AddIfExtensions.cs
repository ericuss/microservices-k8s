
namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;

    public static class AddIfExtensions
    {
        public static IConfigurationBuilder AddIf(this IConfigurationBuilder app, bool include, Func<IConfigurationBuilder, IConfigurationBuilder> action)
        {
            return include ? action(app) : app;
        }

        public static IApplicationBuilder AddIf(this IApplicationBuilder app, bool include, Func<IApplicationBuilder, IApplicationBuilder> action)
        {
            return include ? action(app) : app;
        }

        public static IServiceCollection AddIf(this IServiceCollection services, bool include, Func<IServiceCollection, IServiceCollection> action)
        {
            return include ? action(services) : services;
        }

        public static IMvcCoreBuilder AddIf(this IMvcCoreBuilder services, bool include, Func<IMvcCoreBuilder, IMvcCoreBuilder> action)
        {
            return include ? action(services) : services;
        }

        public static IIdentityServerBuilder AddIf(this IIdentityServerBuilder services, bool include, Func<IIdentityServerBuilder, IIdentityServerBuilder> action)
        {
            return include ? action(services) : services;
        }
    }
}