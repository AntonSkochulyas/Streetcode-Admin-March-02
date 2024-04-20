using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Revoke.RevokeAll
{
    public class RevokeAllHandler : IRequestHandler<RevokeAllCommand, Result<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RevokeAllHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<string>> Handle(RevokeAllCommand request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return Result.Ok("Revoked successfully.");
        }
    }
}
