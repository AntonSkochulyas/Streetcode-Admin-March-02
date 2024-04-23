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

        public RefreshTokenHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<Result<TokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (request.TokenModelDto == null)
            {
                return Result.Fail(UsersErrors.InvalidClientRequestError);
            }

            string? accessToken = request.TokenModelDto.AccessToken;
            string? refreshToken = request.TokenModelDto.RefreshToken;

            var principal = _jwtService.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return Result.Fail(UsersErrors.InvalidAccessOrRefreshTokenError);
            }

            string username = principal.Identity.Name;

            var user = await _userManager.FindByNameAsync(username);

            // TODO: Implement refresh tokens check

            if (user == null)
            {
                return Result.Fail(UsersErrors.InvalidAccessOrRefreshTokenError);
            }

            var newAccessToken = _jwtService.CreateToken(principal.Claims.ToList());
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            // TODO: Implement refresh token storage

            return Result.Ok(new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = DateTime.Now // Fix
            });
        }
    }
}
