using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Streetcode.BLL.Interfaces.Authentification;
using Streetcode.BLL.MediatR.Users.Authorize;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Register.RegisterAdmin
{
    public class RegisterAdminHandler : IRequestHandler<RegisterAdminCommand, Result<Response>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtService _jwtService;

        public RegisterAdminHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        public async Task<Result<Response>> Handle(RegisterAdminCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _userManager.FindByNameAsync(request.RegisterModelDto.Username);
            if (userExists != null)
            {
                return Result.Fail(new Error("User already exists."));
            }

            ApplicationUser user = new ApplicationUser()
            {
                UserAdditionalInfoId = request.RegisterModelDto.UserAdditionalInfoId,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.RegisterModelDto.Username
            };

            var result = await _userManager.CreateAsync(user, request.RegisterModelDto.Password);

            if (!result.Succeeded)
            {
                return Result.Fail(new Error("User creation failed! Please check user details and try again."));
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

            await _jwtService.AuthorizeUserAsync(user);

            return Result.Ok(new Response
            {
                AccessToken = user.AccessToken,
                RefreshToken = user.RefreshToken,
                Expiration = (DateTime)user.AccessTokenExpiryTime,
            });
        }
    }
}
