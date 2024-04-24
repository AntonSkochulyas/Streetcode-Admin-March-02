using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Streetcode.BLL.Dto.Authentication;
using Streetcode.BLL.Interfaces.Authentification;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Refresh
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, Result<TokenDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenService _refreshTokenService;

        public RefreshTokenHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService, IRefreshTokenService refreshTokenService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<Result<TokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (request.TokenModelDto == null)
            {
                return Result.Fail(UsersErrors.InvalidClientRequestError);
            }

            string? accessTokenStr = request.TokenModelDto.AccessToken;
            string? refreshTokenStr = request.TokenModelDto.RefreshToken;

            var principal = _jwtService.GetPrincipalFromExpiredToken(accessTokenStr);
            if (principal == null)
            {
                return Result.Fail(UsersErrors.InvalidAccessOrRefreshTokenError);
            }

            string username = principal.Identity.Name;

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return Result.Fail(UsersErrors.InvalidAccessOrRefreshTokenError);
            }

            var refreshToken = await _refreshTokenService.FindRefreshToken(refreshTokenStr, user.Id);

            if(refreshToken == null)
            {
                return Result.Fail(UsersErrors.InvalidAccessOrRefreshTokenError);
            }

            string newAccessToken = await _jwtService.GenerateAcessTokenAsync(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            refreshToken = await _refreshTokenService.UpdateRefreshToken(refreshTokenStr, newRefreshToken, user.Id);

            if(refreshToken == null)
            {
                return Result.Fail(UsersErrors.InvalidAccessOrRefreshTokenError);
            }

            return Result.Ok(new TokenDto
            {
                AccessToken = newAccessToken,
                RefreshToken = refreshToken.RefreshTokens,
                RefreshTokenExpiration = refreshToken.RefreshTokenExpiryTime
            });
        }
    }
}
