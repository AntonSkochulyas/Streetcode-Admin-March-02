using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Create;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Delete;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.GetAll;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.GetById;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Update;

namespace Streetcode.WebApi.Controllers.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks
{
    /// <summary>
    /// Controller for managing author hyperlinks.
    /// </summary>
    public class AuthorHyperLinkController : BaseApiController
    {
        /// <summary>
        /// Creates a new author hyperlink.
        /// </summary>
        /// <param name="authorHyperLink">The author hyperlink DTO.</param>
        /// <returns>The result of the create operation.</returns>
        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorShipHyperLinkCreateDto authorHyperLink)
        {
            return HandleResult(await Mediator.Send(new CreateAuthorShipHyperLinkCommand(authorHyperLink)));
        }

        /// <summary>
        /// Deletes an author hyperlink by ID.
        /// </summary>
        /// <param name="id">The ID of the author hyperlink to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        [Authorize("Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteAuthorShipHyperLinkCommand(id)));
        }

        /// <summary>
        /// Gets an author hyperlink by ID.
        /// </summary>
        /// <param name="id">The ID of the author hyperlink to retrieve.</param>
        /// <returns>The retrieved hyperlink.</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetAuthorShipHyperLinksByIdQuery(id)));
        }

        /// <summary>
        /// Gets all author hyperlinks.
        /// </summary>
        /// <returns>The list of all hyperlinks.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllAuthorShipHyperLinksQuery()));
        }

        /// <summary>
        /// Updates an existing author hyperlink.
        /// </summary>
        /// <param name="authorHyperLink">The updated author hyperlink DTO.</param>
        /// <returns>The updated hyperlink.</returns>
        [Authorize("Admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AuthorShipHyperLinkDto authorHyperLink)
        {
            return HandleResult(await Mediator.Send(new UpdateAuthorShipHyperLinkCommand(authorHyperLink)));
        }
    }
}
