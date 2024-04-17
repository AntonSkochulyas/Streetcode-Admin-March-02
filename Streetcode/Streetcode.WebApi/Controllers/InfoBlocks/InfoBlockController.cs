using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.InfoBlocks;
using Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Create;
using Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Delete;
using Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.GetAll;
using Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.GetById;
using Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Update;

namespace Streetcode.WebApi.Controllers.InfoBlocks
{
    /// <summary>
    /// Controller for managing InfoBlocks.
    /// </summary>
    public class InfoBlockController : BaseApiController
    {
        /// <summary>
        /// Creates a new InfoBlock.
        /// </summary>
        /// <param name="infoBlock">The InfoBlock to create.</param>
        /// <returns>The created InfoBlock.</returns>
        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InfoBlockCreateDto infoBlock)
        {
            return HandleResult(await Mediator.Send(new CreateInfoBlockCommand(infoBlock)));
        }

        /// <summary>
        /// Deletes an InfoBlock by ID.
        /// </summary>
        /// <param name="id">The ID of the InfoBlock to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        [Authorize("Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteInfoBlockCommand(id)));
        }

        /// <summary>
        /// Gets an InfoBlock by ID.
        /// </summary>
        /// <param name="id">The ID of the InfoBlock to get.</param>
        /// <returns>The InfoBlock with the specified ID.</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetInfoBlockByIdQuery(id)));
        }

        /// <summary>
        /// Gets all InfoBlocks.
        /// </summary>
        /// <returns>All InfoBlocks.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllInfoBlocksQuery()));
        }

        /// <summary>
        /// Updates an existing InfoBlock.
        /// </summary>
        /// <param name="infoBlock">The InfoBlock to update.</param>
        /// <returns>The updated InfoBlock.</returns>
        [Authorize("Admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] InfoBlockDto infoBlock)
        {
            return HandleResult(await Mediator.Send(new UpdateInfoBlockCommand(infoBlock)));
        }
    }
}
