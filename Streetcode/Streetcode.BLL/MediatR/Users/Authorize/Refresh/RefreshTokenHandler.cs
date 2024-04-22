using System.IdentityModel.Tokens.Jwt;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Streetcode.BLL.Dto.Users;
using Streetcode.BLL.Interfaces.Authentification;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Refresh
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, Result<TokenModelDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public RefreshTokenHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<Result<TokenModelDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
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

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return Result.Fail(UsersErrors.InvalidAccessOrRefreshTokenError);
            }

            var newAccessToken = _jwtService.CreateToken(principal.Claims.ToList());
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new TokenModelDto()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
            };
        }
    }
}
