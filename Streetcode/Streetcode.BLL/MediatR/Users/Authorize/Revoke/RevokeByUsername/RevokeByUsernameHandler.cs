using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Revoke.RevokeByUsername
{
    internal class RevokeByUsernameHandler : IRequestHandler<RevokeByUsernameCommand, Result<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RevokeByUsernameHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<string>> Handle(RevokeByUsernameCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return Result.Fail(UsersErrors.InvalidUsernameError);
            }

            // TODO: Implement refresh token delete

            return Result.Ok("Revoked successfully.");
        }
    }
}
