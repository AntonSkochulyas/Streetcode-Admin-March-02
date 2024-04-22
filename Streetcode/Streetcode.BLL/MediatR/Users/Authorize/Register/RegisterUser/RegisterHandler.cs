using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Streetcode.BLL.Interfaces.Authentification;
using Streetcode.BLL.MediatR.Users.Authorize;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Register.RegisterUser
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<Response>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public RegisterHandler(UserManager<ApplicationUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<Result<Response>> Handle(RegisterCommand request, CancellationToken cancellationToken)
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
