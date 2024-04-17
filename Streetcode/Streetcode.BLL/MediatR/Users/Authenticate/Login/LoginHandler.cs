using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Streetcode.BLL.Interfaces.Authentification;
using Streetcode.BLL.Services.Authentification;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, Result<Response>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;

        public LoginHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService, IConfiguration configuration)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        public async Task<Result<Response>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.LoginModelDto.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, request.LoginModelDto.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var accessToken = _jwtService.CreateToken(authClaims);
                var refreshToken = _jwtService.GenerateRefreshToken();
                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
                _ = int.TryParse(_configuration["JWT:AccessTokenValidityInMinutes"], out int accessTokenValidityInMinutes);

                user.AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken);
                user.AccessTokenExpiryTime = DateTime.Now.AddMinutes(accessTokenValidityInMinutes);

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

                await _userManager.UpdateAsync(user);

                return Result.Ok(new Response
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                    RefreshToken = refreshToken,
                    Expiration = accessToken.ValidTo,
                });
            }

            return Result.Fail("Can not login to the system.");
        }
    }
}