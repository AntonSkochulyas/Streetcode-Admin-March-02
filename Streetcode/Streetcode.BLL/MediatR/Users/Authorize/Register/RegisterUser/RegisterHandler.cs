using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Streetcode.BLL.Dto.Authentication;
using Streetcode.BLL.Interfaces.Authentification;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Register.RegisterUser
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<TokenDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenService _refreshTokenService;

        public RegisterHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService, IRefreshTokenService refreshTokenService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<Result<TokenDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _userManager.FindByNameAsync(request.RegisterModelDto.Username);
            if (userExists != null)
            {
                return Result.Fail(new Error(UsersErrors.UserAlreadyExistsError));
            }

            ApplicationUser user = new ApplicationUser()
            {
                UserAdditionalInfoId = request.RegisterModelDto.UserAdditionalInfoId,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.RegisterModelDto.Username,
            };

            var result = await _userManager.CreateAsync(user, request.RegisterModelDto.Password);

            if (!result.Succeeded)
            {
                return Result.Fail(new Error(UsersErrors.UserCreationFailureError));
            }

            string accessTokenStr = await _jwtService.GenerateAcessTokenAsync(user);
            string refreshTokenStr = _jwtService.GenerateRefreshToken();

            var refreshToken = await _refreshTokenService.SaveRefreshToken(refreshTokenStr, user.Id);

            if (refreshToken == null)
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
    }
}
