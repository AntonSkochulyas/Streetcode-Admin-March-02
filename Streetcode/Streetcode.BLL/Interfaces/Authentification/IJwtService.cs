using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Streetcode.BLL.Dto;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.Interfaces.Authentification
{
    public interface IJwtService
    {
        JwtSecurityToken CreateToken(List<Claim> authClaims);

        string GenerateRefreshToken();

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
