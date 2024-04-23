using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Streetcode.BLL.Dto.Authentication;
using Streetcode.BLL.Interfaces.Authentification;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, Result<TokenDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenService _refreshTokenService;

        public LoginHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService, IRefreshTokenService refreshTokenService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<Result<TokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.LoginModelDto.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, request.LoginModelDto.Password))
            {
                string accessTokenStr = await _jwtService.GenerateAcessTokenAsync(user);
                string refreshTokenStr = _jwtService.GenerateRefreshToken();

                var refreshToken = await _refreshTokenService.SaveRefreshToken(refreshTokenStr, user.Id);

                if(refreshToken == null)
                {
                    return Result.Fail(UsersErrors.CanNotLoginError);
                }

                return Result.Ok(new TokenDto
                {
                    AccessToken = accessTokenStr,
                    RefreshToken = refreshToken.RefreshTokens,
                    RefreshTokenExpiration = refreshToken.RefreshTokenExpiryTime
                });
            }

            return Result.Fail(UsersErrors.CanNotLoginError);
        }
    }
}