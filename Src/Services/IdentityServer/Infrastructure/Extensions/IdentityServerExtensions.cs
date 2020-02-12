namespace Microsoft.Extensions.DependencyInjection
{
    using IdentityServer;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using System;

    public static class IdentityServerExtensions
    {
        public static IServiceCollection AddCustomIdentityServer(this IServiceCollection services, IWebHostEnvironment env ){
            return services
                .AddIdentityServer()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(Config.GetUsers())
                .AddIf(env.IsDevelopment(), x => x.AddDeveloperSigningCredential())
                .AddIf(!env.IsDevelopment(), x => throw new Exception("need to configure key material"))
                .Services
                ;
        }
    }
}