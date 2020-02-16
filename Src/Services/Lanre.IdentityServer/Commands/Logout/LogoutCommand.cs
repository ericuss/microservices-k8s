namespace Lanre.IdentityServer.Commands.Logout
{
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    public class LogoutCommand : IRequest<LogoutResponse>
    {
        public string LogoutId { get; set; }

        public ClaimsPrincipal User { get; set; }

        public HttpContext HttpContext { get; set; }
    }
}