using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Streetcode.BLL.Interfaces.Authentification;
using Streetcode.BLL.MediatR.Users.Authorize;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, Result<Response>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public LoginHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<Result<Response>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.LoginModelDto.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, request.LoginModelDto.Password))
            {
                await _jwtService.AuthorizeUserAsync(user);

                return Result.Ok(new Response
                {
                    AccessToken = user.AccessToken,
                    RefreshToken = user.RefreshToken,
                    Expiration = (DateTime)user.AccessTokenExpiryTime,
                });
            }

            return Result.Fail(UsersErrors.CanNotLoginError);
        }
    }
}