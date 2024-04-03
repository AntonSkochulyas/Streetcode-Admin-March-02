using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.InfoBlocks;
using Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Create;
using Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Delete;
using Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.GetAll;
using Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.GetById;
using Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Update;

namespace Streetcode.WebApi.Controllers.InfoBlocks
{
    public class InfoBlockController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InfoBlockDto infoBlock)
        {
            return HandleResult(await Mediator.Send(new CreateInfoBlockCommand(infoBlock)));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteInfoBlockCommand(id)));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetInfoBlockByIdQuery(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllInfoBlocksQuery()));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] InfoBlockDto infoBlock)
        {
            return HandleResult(await Mediator.Send(new UpdateInfoBlockCommand(infoBlock)));
        }
    }
}
