// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Lanre.IdentityServer
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Hosting;
    using System.Reflection;
    using Lanre.IdentityServer.Commands.Logout;
    using MediatR;

    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment environment)
        {
            _env = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMediatR(Assembly.GetAssembly(typeof(LogoutCommandHandler)))
                .AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .Services
                .AddCustomIdentityServer(_env)
                .AddCustomAuthentication()
                .AddCustomHealthChecks()
                ;
        }

        public void Configure(IApplicationBuilder app)
        {
            app
                .AddIf(_env.IsDevelopment(), x => x.UseDeveloperExceptionPage())
                .UseStaticFiles()
                .UseIdentityServer()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.UseHealthChecks();
                    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                });
        }
    }
}