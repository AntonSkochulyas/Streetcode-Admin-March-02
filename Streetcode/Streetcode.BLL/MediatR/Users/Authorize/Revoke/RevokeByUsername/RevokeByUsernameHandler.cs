using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Streetcode.BLL.Interfaces.Authentification;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Revoke.RevokeByUsername
{
    internal class RevokeByUsernameHandler : IRequestHandler<RevokeByToken, Result<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenService _refreshTokenService;

        public RevokeByUsernameHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService, IRefreshTokenService refreshTokenService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<Result<string>> Handle(RevokeByToken request, CancellationToken cancellationToken)
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

            var res = await _refreshTokenService.DeleteRefreshToken(refreshTokenStr, user.Id);

            if(res == null)
            {
                return Result.Fail(UsersErrors.InvalidAccessOrRefreshTokenError);
            }

            return Result.Ok("Revoked successfully.");
        }
    }
}
