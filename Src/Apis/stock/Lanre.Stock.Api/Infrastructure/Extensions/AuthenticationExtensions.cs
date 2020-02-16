// Copyright (c) Lanre. All rights reserved.

using Lanre.Stock.Api.Infrastructure.Settings;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IdentitySettings identitySettings)
        {
            services.AddAuthentication("Bearer")
                    .AddJwtBearer("Bearer", options =>
                    {
                        options.Audience = identitySettings.Audience;
                        options.Authority = identitySettings.Url;
                        options.RequireHttpsMetadata = false;
                    });

            return services;
        }
    }
}
