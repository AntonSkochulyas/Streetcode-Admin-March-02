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

        public LoginHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<Result<TokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.LoginModelDto.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, request.LoginModelDto.Password))
            {
                string accessToken = await _jwtService.GenerateAcessTokenAsync(user);
                string refreshToken = _jwtService.GenerateRefreshToken();

                // TODO: Implement refresh token storage

                return Result.Ok(new TokenDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiration = DateTime.Now // Fix
                });
            }

            return Result.Fail(UsersErrors.CanNotLoginError);
        }
    }
}