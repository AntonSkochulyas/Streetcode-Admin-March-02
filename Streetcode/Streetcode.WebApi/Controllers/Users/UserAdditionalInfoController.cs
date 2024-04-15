using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Users;
using Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.Create;
using Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.Delete;
using Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.GetAll;
using Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.GetById;
using Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.Update;

namespace Streetcode.WebApi.Controllers.Users
{
    public class UserAdditionalInfoController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserAdditionalInfoDto userAdditionalInfoDto)
        {
            return HandleResult(await Mediator.Send(new CreateUserAdditionalInfoCommand(userAdditionalInfoDto)));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllUserAdditionalInfoQuery()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return HandleResult(await Mediator.Send(new GetByIdUserAdditionalInfoQeury(id)));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return HandleResult(await Mediator.Send(new DeleteUserAdditionalInfoCommand(id)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserAdditionalInfoDto userAdditionalInfoDto)
        {
            return HandleResult(await Mediator.Send(new UpdateUserAdditionalInfoCommand(userAdditionalInfoDto)));
        }
    }
}
