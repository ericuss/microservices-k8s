namespace Microsoft.Extensions.DependencyInjection
{
    using IdentityServer4;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.IdentityModel.Tokens;

    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
        {
            return services.AddAuthentication()
                .AddOpenIdConnect("oidc", "Demo IdentityServer", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                    options.SaveTokens = true;

                    options.Authority = "https://demo.identityserver.io/";
                    options.ClientId = "native.code";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                })
                .Services
                ;
        }
        public static IApplicationBuilder UseCustomIdentityServer(this IApplicationBuilder app)
        {
            return app.UseIdentityServer();
        }
    }
}