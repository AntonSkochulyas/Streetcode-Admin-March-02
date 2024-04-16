using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Logout
{
    public class LogoutHandler : IRequestHandler<LogoutCommand,Result<string>>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutHandler(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<Result<string>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();

            return Result.Ok("Logout successfully.");
        }
    }
}
