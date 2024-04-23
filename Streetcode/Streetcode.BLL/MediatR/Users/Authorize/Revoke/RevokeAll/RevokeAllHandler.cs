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

            // TODO: Implement refresh token delete

            return Result.Ok("Revoked successfully.");
        }
    }
}
