using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto;
using Streetcode.BLL.Dto.Authentication;
using Streetcode.BLL.Interfaces.Authentification;
using Streetcode.DAL.Entities.Users;
using System.Security.Claims;

namespace Streetcode.WebApi.Controllers.Auth
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authenticationResponse = _jwtService.CreateToken(user, userRoles);
                user.RefreshToken = authenticationResponse.RefreshToken;
                user.RefreshTokenExpirationDate = (DateTime)authenticationResponse.RefreshTokenExpirationDate;

                await _userManager.UpdateAsync(user);

                return Ok(authenticationResponse);
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                string errorMessage = string.Join(" | ", ModelState.Values.SelectMany(x => x.Errors).Select(e => e.ErrorMessage));
                return Problem(errorMessage);
            }

            var isUserExists = await _userManager.FindByNameAsync(model.Username);

            if (isUserExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists." });
            }

            ApplicationUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
            };

            var isRegisteredSuccessfully = await _userManager.CreateAsync(user, model.Password);

            if (!isRegisteredSuccessfully.Succeeded)
            {
                string errorMessage = string.Join(" | ", isRegisteredSuccessfully.Errors.Select(e => e.Description));

                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = errorMessage });
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            var userRoles = await _userManager.GetRolesAsync(user);
            var authenticationResponse = _jwtService.CreateToken(user, userRoles);
            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpirationDate = (DateTime)authenticationResponse.RefreshTokenExpirationDate;

            await _userManager.UpdateAsync(user);

            return Ok(authenticationResponse);
        }

        [HttpPost]
        [Route("generate-new-refresh-token")]
        public async Task<IActionResult> GenerateNewRefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            ClaimsPrincipal? principal = _jwtService.GetPrincipalFromJwtToken(tokenModel.AccessToken);
            if (principal is null)
            {
                return BadRequest("Invalid jwt access token");
            }

            // TO-DO: Probably the code below should be changed after using the UserAdditionalInfo entity
            string? userName = principal.FindFirstValue(ClaimTypes.Name);

            ApplicationUser? user = await _userManager.FindByNameAsync(userName);

            if (user is null || user.RefreshToken != tokenModel.RefreshToken ||
                user.RefreshTokenExpirationDate <= DateTime.Now)
            {
                return BadRequest("Invalid refresh token");
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            AuthenticationResponse authenticationResponse = _jwtService.CreateToken(user, userRoles);
            user.RefreshToken = authenticationResponse.RefreshToken;
            user.RefreshTokenExpirationDate = (DateTime)authenticationResponse.RefreshTokenExpirationDate;

            await _userManager.UpdateAsync(user);

            return Ok(authenticationResponse);
        }

        [HttpGet]
        public async Task<IActionResult> IsAlreadyRegistered(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest("Invalid user name");
            }

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return NoContent();
        }

        [Authorize]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return NoContent();
        }
    }
}
