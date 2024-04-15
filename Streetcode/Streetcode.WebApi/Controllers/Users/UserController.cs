using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.MediatR.Users.User.GetAll;

namespace Streetcode.WebApi.Controllers.Users
{
    public class UserController : BaseApiController
    {
        // Get all
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllApplicationUserQuery()));
        }
    }
}
