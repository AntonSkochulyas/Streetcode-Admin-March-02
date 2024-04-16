using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Register.RegisterUser
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<Response>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RegisterHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Result<Response>> Handle(RegisterCommand request, CancellationToken cancellationToken)
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
                UserName = request.RegisterModelDto.Username,
            };

            var result = await _userManager.CreateAsync(user, request.RegisterModelDto.Password);

            if (!result.Succeeded)
            {
                return Result.Fail(new Error("User creation failed! Please check user details and try again."));
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            return Result.Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
    }
}
