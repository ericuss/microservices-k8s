namespace Lanre.IdentityServer.Queries.GetConsent
{
    using CSharpFunctionalExtensions;
    using IdentityServer.Models.Consent;
    using IdentityServer4.Models;
    using IdentityServer4.Services;
    using IdentityServer4.Stores;
    using MediatR;
    using Microsoft.AspNetCore.Authentication;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class ConsentQueryHandler : IRequestHandler<ConsentQuery, Result<ConsentQueryResponse>>
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IResourceStore _resourceStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;

        public ConsentQueryHandler(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IResourceStore resourceStore,
            IAuthenticationSchemeProvider schemeProvider)
        {
            this._interaction = interaction;
            this._clientStore = clientStore;
            this._resourceStore = resourceStore;
            this._schemeProvider = schemeProvider;
        }

        public async Task<Result<ConsentQueryResponse>> Handle(ConsentQuery requestQuery, CancellationToken cancellationToken)
        {
            var request = await _interaction.GetAuthorizationContextAsync(requestQuery.ReturnUrl);
            if (request == null)
            {
                return Result.Failure<ConsentQueryResponse>($"No consent request matching request: {requestQuery.ReturnUrl}");
            }

            var client = await _clientStore.FindEnabledClientByIdAsync(request.ClientId);
            if (client == null)
            {
                return Result.Failure<ConsentQueryResponse>($"Invalid client id: {request.ClientId}");
            }
            var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(request.ScopesRequested);
            if (resources != null && (resources.IdentityResources.Any() || resources.ApiResources.Any()))
            {
                return CreateConsentViewModel(requestQuery.Model, requestQuery.ReturnUrl, request, client, resources);
            }
            else
            {
                return Result.Failure<ConsentQueryResponse>($"No scopes matching: {request.ScopesRequested.Aggregate((x, y) => x + ", " + y)}");
            }
        }

        private Result<ConsentQueryResponse> CreateConsentViewModel(
          ConsentInputModel model,
          string returnUrl,
          AuthorizationRequest request,
          Client client, Resources resources)
        {
            var vm = new ConsentQueryResponse
            {
                RememberConsent = model?.RememberConsent ?? true,
                ScopesConsented = model?.ScopesConsented ?? Enumerable.Empty<string>(),

                ReturnUrl = returnUrl,

                ClientName = client.ClientName ?? client.ClientId,
                ClientUrl = client.ClientUri,
                ClientLogoUrl = client.LogoUri,
                AllowRememberConsent = client.AllowRememberConsent
            };

            vm.IdentityScopes = resources.IdentityResources.Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null)).ToArray();
            vm.ResourceScopes = resources.ApiResources.SelectMany(x => x.Scopes).Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null)).ToArray();
            if (ConsentOptions.EnableOfflineAccess && resources.OfflineAccess)
            {
                vm.ResourceScopes = vm.ResourceScopes.Union(new ScopeQueryResponse[] {
                    GetOfflineAccessScope(vm.ScopesConsented.Contains(IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess) || model == null)
                });
            }

            return Result.Ok(vm);
        }

        private ScopeQueryResponse CreateScopeViewModel(IdentityResource identity, bool check)
        {
            return new ScopeQueryResponse
            {
                Name = identity.Name,
                DisplayName = identity.DisplayName,
                Description = identity.Description,
                Emphasize = identity.Emphasize,
                Required = identity.Required,
                Checked = check || identity.Required
            };
        }

        private ScopeQueryResponse CreateScopeViewModel(Scope scope, bool check)
        {
            return new ScopeQueryResponse
            {
                Name = scope.Name,
                DisplayName = scope.DisplayName,
                Description = scope.Description,
                Emphasize = scope.Emphasize,
                Required = scope.Required,
                Checked = check || scope.Required
            };
        }
        private ScopeQueryResponse GetOfflineAccessScope(bool check)
        {
            return new ScopeQueryResponse
            {
                Name = IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess,
                DisplayName = ConsentOptions.OfflineAccessDisplayName,
                Description = ConsentOptions.OfflineAccessDescription,
                Emphasize = true,
                Checked = check
            };
        }
    }
}
