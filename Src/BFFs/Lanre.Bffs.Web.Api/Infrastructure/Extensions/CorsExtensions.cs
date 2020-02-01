// Copyright (c) Lanre. All rights reserved.

namespace Microsoft.Extensions.DependencyInjection
{
    using Microsoft.AspNetCore.Builder;

    public static class CorsExtensions
    {
        private static readonly string CORSPOLICY = "default";
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            return services.AddCors(x => x.AddPolicy(CORSPOLICY, c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
        }

        public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
        {
            return app.UseCors(CORSPOLICY);
        }
    }
}