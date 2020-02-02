// Copyright (c) Lanre. All rights reserved.

namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.OpenApi.Models;

    public static class SwaggerExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
                {
                    setup.DescribeAllParametersInCamelCase();
                    setup.DescribeStringEnumsInCamelCase();
                    setup.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Lanre Products Api",
                        Version = "v1",
                    });

                    setup.CustomSchemaIds(x => x.FullName);
                });

            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            return app.UseSwagger()
                      .UseSwaggerUI(setup =>
                      {
                          setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Lanre Products");
                          setup.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                      });
        }
    }
}