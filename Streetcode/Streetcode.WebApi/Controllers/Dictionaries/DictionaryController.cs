using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.BLL.MediatR.Dictionaries.Create;
using Streetcode.BLL.MediatR.Dictionaries.Delete;
using Streetcode.BLL.MediatR.Dictionaries.GetAll;
using Streetcode.BLL.MediatR.Dictionaries.GetById;
using Streetcode.BLL.MediatR.Dictionaries.Update;

namespace Streetcode.WebApi.Controllers.Dictionaries
{
    /// <summary>
    /// Represents a controller for managing dictionary items.
    /// </summary>
    public class DictionaryController : BaseApiController
    {
        /// <summary>
        /// Creates a new dictionary item.
        /// </summary>
        /// <param name="dictionaryItem">The dictionary item to create.</param>
        /// <returns>Dictionary item.</returns>
        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DictionaryItemDto dictionaryItem)
        {
            return HandleResult(await Mediator.Send(new CreateDictionaryItemCommand(dictionaryItem)));
        }

        /// <summary>
        /// Deletes a dictionary item by its ID.
        /// </summary>
        /// <param name="id">The ID of the dictionary item to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        [Authorize("Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteDictionaryItemCommand(id)));
        }

        /// <summary>
        /// Retrieves a dictionary item by its ID.
        /// </summary>
        /// <param name="id">The ID of the dictionary item to retrieve.</param>
        /// <returns>Dictionary item.</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetDictionaryItemByIdQuery(id)));
        }

        /// <summary>
        /// Retrieves all dictionary items.
        /// </summary>
        /// <returns>Dictionary items.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllDictionaryItemsQuery()));
        }

        /// <summary>
        /// Updates a dictionary item.
        /// </summary>
        /// <param name="dictionaryItem">The dictionary item to update.</param>
        /// <returns>Updated dictionary item.</returns>
        [Authorize("Admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DictionaryItemDto dictionaryItem)
        {
            return HandleResult(await Mediator.Send(new UpdateDictionaryItemCommand(dictionaryItem)));
        }
    }
}
