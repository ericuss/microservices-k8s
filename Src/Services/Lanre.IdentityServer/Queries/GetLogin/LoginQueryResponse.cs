namespace Lanre.IdentityServer.Queries.GetLogin
{
    public class LoginQueryResponse
    {
        public string Username { get; set; }

        public string ReturnUrl { get; set; }

        public bool AllowRememberLogin { get; set; } = true;
    }
}
