// Necessary usings
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
    /// <summary>
    /// Controller for managing authorization.
    /// Handles endpoints for:
    /// - Login;
    /// - Logout;
    /// - Register;
    /// - RegisterAdmin;
    /// - RefreshToken;
    /// - RevokeByUsername;
    /// - RevokeAll.
    /// </summary>
    [AllowAnonymous]
    public class AuthenticateController : BaseApiController
    {
        /// <summary>
        /// Login endpoint.
        /// </summary>
        /// <returns>A response, that contains: access token, refresh token and refresh token expiration date.</returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModelDto loginModelDto)
        {
            return HandleResult(await Mediator.Send(new LoginCommand(loginModelDto)));
        }

        /// <summary>
        /// Logout endpoint.
        /// </summary>
        /// <returns>A message, that user logout successfully.</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            return HandleResult(await Mediator.Send(new LogoutCommand()));
        }

        /// <summary>
        /// Register endpoint.
        /// </summary>
        /// <returns>A response, that contains status and message for registration operation.</returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModelDto registerModelDto)
        {
            return HandleResult(await Mediator.Send(new RegisterCommand(registerModelDto)));
        }

        /// <summary>
        /// Register admin endpoint.
        /// </summary>
        /// <returns>A response, that contains status and message for admin registration operation.</returns>
        [HttpPost]
        [Authorize("Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModelDto registerModelDto)
        {
            return HandleResult(await Mediator.Send(new RegisterAdminCommand(registerModelDto)));
        }

        /// <summary>
        /// Refresh token endpoint.
        /// </summary>
        /// <returns>A response, that contains a new access and refresh token.</returns>
        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] TokenModelDto tokenModelDto)
        {
            return HandleResult(await Mediator.Send(new RefreshTokenCommand(tokenModelDto)));
        }

        /// <summary>
        /// Revoke by username endpoint.
        /// </summary>
        /// <returns>A message, that token revoked successfully or eror, if it was.</returns>
        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> RevokeByUsername([FromBody] string username)
        {
            return HandleResult(await Mediator.Send(new RevokeByUsernameCommand(username)));
        }

        /// <summary>
        /// Revoke by username endpoint.
        /// </summary>
        /// <returns>A message, that token revoked successfully or eror, if it was.</returns>
        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> RevokeAll()
        {
            return HandleResult(await Mediator.Send(new RevokeAllCommand()));
        }
    }
}
