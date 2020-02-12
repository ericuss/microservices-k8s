namespace IdentityServer.Commands.Login
{
    using CSharpFunctionalExtensions;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;

    public class LoginCommand : IRequest<Result<LoginResponse>>
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public bool RememberLogin { get; set; }
        
        public string ReturnUrl { get; set; }

        public HttpContext HttpContext { get; set; }
    }
}