using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Create;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Delete;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.GetAll;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.GetById;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Update;

namespace Streetcode.WebApi.Controllers.InfoBlocks.AuthorsInfoes
{
    public class AuthorShipController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorShipDto authorShip)
        {
            return HandleResult(await Mediator.Send(new CreateAuthorShipCommand(authorShip)));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteAuthorShipCommand(id)));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetAuthorShipByIdQuery(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllAuthorShipsQuery()));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AuthorShipDto authorShip)
        {
            return HandleResult(await Mediator.Send(new UpdateAuthorShipCommand(authorShip)));
        }
    }
}
