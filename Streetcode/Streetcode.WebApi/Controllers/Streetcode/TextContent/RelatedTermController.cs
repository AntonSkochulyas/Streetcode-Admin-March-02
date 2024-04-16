using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Streetcode.TextContent;
using Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Create;
using Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Delete;
using Streetcode.BLL.MediatR.Streetcode.RelatedTerm.GetAllByTermId;
using Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Update;

namespace Streetcode.WebApi.Controllers.Streetcode.TextContent
{
    /// <summary>
    /// Controller for managing related terms.
    /// </summary>
    public class RelatedTermController : BaseApiController
    {
        /// <summary>
        /// Get all related terms by term ID.
        /// </summary>
        /// <param name="id">The term ID.</param>
        /// <returns>The related terms.</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByTermId([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetAllRelatedTermsByTermIdQuery(id)));
        }

        /// <summary>
        /// Create a new related term.
        /// </summary>
        /// <param name="relatedTerm">The related term to create.</param>
        /// <returns>The created related term.</returns>
        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RelatedTermDto relatedTerm)
        {
            return HandleResult(await Mediator.Send(new CreateRelatedTermCommand(relatedTerm)));
        }

        /// <summary>
        /// Update an existing related term.
        /// </summary>
        /// <param name="id">The ID of the related term to update.</param>
        /// <param name="relatedTerm">The related term to update.</param>
        /// <returns>The updated related term.</returns>
        [Authorize("Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RelatedTermDto relatedTerm)
        {
            return HandleResult(await Mediator.Send(new UpdateRelatedTermCommand(id, relatedTerm)));
        }

        /// <summary>
        /// Delete a related term by word.
        /// </summary>
        /// <param name="word">The word of the related term to delete.</param>
        /// <returns>The result of the deletion.</returns>
        [Authorize("Admin")]
        [HttpDelete("{word}")]
        public async Task<IActionResult> Delete([FromRoute] string word)
        {
            return HandleResult(await Mediator.Send(new DeleteRelatedTermCommand(word)));
        }
    }
}
