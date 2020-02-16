namespace Lanre.IdentityServer.Commands.Login
{
    using IdentityServer4.Services;
    using System.Threading.Tasks;
    using System.Threading;
    using MediatR;
    using IdentityServer4.Test;
    using IdentityServer4.Events;
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Authentication;
    using CSharpFunctionalExtensions;

    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {

        private readonly TestUserStore _users;
        public LoginCommandHandler(
            TestUserStore users = null)
        {
            this._users = users ?? new TestUserStore(TestUsers.Users);
        }
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            if (!_users.ValidateCredentials(request.Username, request.Password))
            {
                return Result.Failure<LoginResponse>("User/pass invalid");
            }

            var user = _users.FindByUsername(request.Username);

            // only set explicit expiration here if user chooses "remember me". 
            // otherwise we rely upon expiration configured in cookie middleware.
            AuthenticationProperties props = null;
            if (AccountOptions.AllowRememberLogin && request.RememberLogin)
            {
                props = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                };
            };

            // issue authentication cookie with subject ID and username
            await request.HttpContext.SignInAsync(user.SubjectId, user.Username, props);
            return Result.Ok(new LoginResponse());
        }
    }
}