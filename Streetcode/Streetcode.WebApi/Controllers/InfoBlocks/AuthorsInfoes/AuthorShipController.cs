using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Create;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Delete;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.GetAll;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.GetById;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Update;

namespace Streetcode.WebApi.Controllers.InfoBlocks.AuthorsInfoes
{
    /// <summary>
    /// Controller for managing authorship information.
    /// </summary>
    public class AuthorShipController : BaseApiController
    {
        /// <summary>
        /// Creates a new authorship.
        /// </summary>
        /// <param name="authorShip">The authorship data.</param>
        /// <returns>The created authorship.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorShipDto authorShip)
        {
            return HandleResult(await Mediator.Send(new CreateAuthorShipCommand(authorShip)));
        }

        /// <summary>
        /// Deletes an authorship by ID.
        /// </summary>
        /// <param name="id">The ID of the authorship to delete.</param>
        /// <returns>The result of the deletion operation.</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteAuthorShipCommand(id)));
        }

        /// <summary>
        /// Gets an authorship by ID.
        /// </summary>
        /// <param name="id">The ID of the authorship to retrieve.</param>
        /// <returns>The retrieved authorship.</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetAuthorShipByIdQuery(id)));
        }

        /// <summary>
        /// Gets all authorships.
        /// </summary>
        /// <returns>The list of all authorships.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllAuthorShipsQuery()));
        }

        /// <summary>
        /// Updates an existing authorship.
        /// </summary>
        /// <param name="authorShip">The updated authorship data.</param>
        /// <returns>The updated authorship.</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AuthorShipDto authorShip)
        {
            return HandleResult(await Mediator.Send(new UpdateAuthorShipCommand(authorShip)));
        }
    }
}
