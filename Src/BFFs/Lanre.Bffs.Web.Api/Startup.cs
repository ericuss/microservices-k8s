namespace Lanre.BFFs.Web.Api
{
    using Lanre.Bffs.Web.Api.Infrastructure.Settings;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Collections.Generic;

    public class Startup
    {
        public Startup(IHostEnvironment env)
        {
            var configBuilder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables()
                ;

            Configuration = configBuilder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<List<ApiSettings>>(this.Configuration.GetSection("Apis"))
                .AddControllers()
                .Services
                .AddCustomHealthChecks(this.Configuration)
                .AddCustomSwagger()
                .AddCustomCors()
                .RegisterServices()
                ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseCustomCors()
                .UseHttpsRedirection()
                .UseRouting()
                .UseCustomSwagger()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.UseHealthChecks();
                    endpoints.MapControllers();
                });
        }
    }
}
