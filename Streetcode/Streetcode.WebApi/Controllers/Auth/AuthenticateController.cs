using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Users;
using Streetcode.BLL.MediatR.Users.Authenticate.Login;
using Streetcode.BLL.MediatR.Users.Authenticate.Logout;
using Streetcode.BLL.MediatR.Users.Authenticate.Refresh;
using Streetcode.BLL.MediatR.Users.Authenticate.Register.RegisterAdmin;
using Streetcode.BLL.MediatR.Users.Authenticate.Register.RegisterUser;
using Streetcode.BLL.MediatR.Users.Authenticate.Revoke.RevokeAll;
using Streetcode.BLL.MediatR.Users.Authenticate.Revoke.RevokeByUsername;

namespace Streetcode.WebApi.Controllers.Auth
{
    [AllowAnonymous]
    public class AuthenticateController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModelDto loginModelDto)
        {
            return HandleResult(await Mediator.Send(new LoginCommand(loginModelDto)));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            return HandleResult(await Mediator.Send(new LogoutCommand()));
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModelDto registerModelDto)
        {
            return HandleResult(await Mediator.Send(new RegisterCommand(registerModelDto)));
        }

        [HttpPost]
        [Authorize("Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModelDto registerModelDto)
        {
            return HandleResult(await Mediator.Send(new RegisterAdminCommand(registerModelDto)));
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModelDto tokenModelDto)
        {
            return HandleResult(await Mediator.Send(new RefreshTokenCommand(tokenModelDto)));
        }

        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> RevokeByUsername([FromBody] string username)
        {
            return HandleResult(await Mediator.Send(new RevokeByUsernameCommand(username)));
        }

        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> RevokeAll()
        {
            return HandleResult(await Mediator.Send(new RevokeAllCommand()));
        }
    }
}
