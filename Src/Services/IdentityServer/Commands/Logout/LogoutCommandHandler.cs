namespace IdentityServer.Commands.Logout
{
    using IdentityModel;
    using IdentityServer4.Extensions;
    using IdentityServer4.Services;
    using System.Threading.Tasks;
    using System.Threading;
    using MediatR;

    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, LogoutResponse>
    {

        private readonly IIdentityServerInteractionService _interaction;

        public async Task<LogoutResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(request.LogoutId);

            var vm = new LogoutResponse
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = request.LogoutId
            };

            if (request.User?.Identity.IsAuthenticated == true)
            {
                var idp = request.User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignout = await request.HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        if (vm.LogoutId == null)
                        {
                            // if there's no current logout context, we need to create one
                            // this captures necessary info from the current logged in user
                            // before we signout and redirect away to the external IdP for signout
                            vm.LogoutId = await _interaction.CreateLogoutContextAsync();
                        }
                    }
                }
            }

            return vm;
        }
    }
}