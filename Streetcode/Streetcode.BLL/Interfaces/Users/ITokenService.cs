using System.IdentityModel.Tokens.Jwt;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.Interfaces.Users
{
    public interface ITokenService
    {
        public JwtSecurityToken GenerateJWTToken(UserAdditionalInfo user);
        public JwtSecurityToken RefreshToken(string token);
    }
}
