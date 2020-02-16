namespace Lanre.IdentityServer.Queries.GetConsent
{
    using CSharpFunctionalExtensions;
    using IdentityServer.Models.Consent;
    using MediatR;

    public class ConsentQuery : IRequest<Result<ConsentQueryResponse>>
    {
        public string ReturnUrl { get; set; }

        public ConsentInputModel Model { get; internal set; }
    }
}
