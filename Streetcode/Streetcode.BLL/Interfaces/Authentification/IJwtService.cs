﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Streetcode.BLL.Dto;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.Interfaces.Authentification
{
    public interface IJwtService
    {
        AuthentificationResponseDto CreateToken(ApplicationUser user, IList<string> userRoles);
    }
}
