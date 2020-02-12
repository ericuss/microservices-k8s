namespace IdentityServer.Queries.GetLogin
{
    using MediatR;

    public class LoginQuery : IRequest<LoginQueryResponse>
    {
        public string ReturnUrl { get; set; }
    }
}
