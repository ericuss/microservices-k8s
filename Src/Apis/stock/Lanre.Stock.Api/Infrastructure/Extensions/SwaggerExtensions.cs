// Copyright (c) Lanre. All rights reserved.

namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.OpenApi.Models;
    using System;
    using System.Collections.Generic;

    public static class SwaggerExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, string urlIdentity)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Lanre Stock Api",
                    Version = "v1"
                });

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{urlIdentity}/connect/authorize"),
                            TokenUrl = new Uri($"{urlIdentity}/connect/token"),
                            Scopes = new Dictionary<string, string>()
                            {
                                { "stock", "Stock API" }
                            }
                        }
                    }
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });


            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            return app.UseSwagger()
                      .UseSwaggerUI(setup =>
                      {
                          setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Lanre Stock");
                          setup.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                          setup.OAuthClientId("stockswaggerui");
                          setup.OAuthAppName("Stock Swagger UI");
                      });
        }
    }
}