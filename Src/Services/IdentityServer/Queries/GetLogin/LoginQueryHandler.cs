namespace IdentityServer.Queries.GetLogin
{
    using IdentityServer4.Services;
    using IdentityServer4.Stores;
    using MediatR;
    using Microsoft.AspNetCore.Authentication;
    using System.Threading;
    using System.Threading.Tasks;

    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginQueryResponse>
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;

        public LoginQueryHandler(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider)
        {
            this._interaction = interaction;
            this._clientStore = clientStore;
            this._schemeProvider = schemeProvider;
        }

        public async Task<LoginQueryResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var context = await _interaction.GetAuthorizationContextAsync(request.ReturnUrl);

            return new LoginQueryResponse
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                ReturnUrl = request.ReturnUrl,
                Username = context?.LoginHint
            };
        }
    }
}
