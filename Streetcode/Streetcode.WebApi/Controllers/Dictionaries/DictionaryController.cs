using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.BLL.MediatR.Dictionaries.Create;
using Streetcode.BLL.MediatR.Dictionaries.Delete;
using Streetcode.BLL.MediatR.Dictionaries.GetAll;
using Streetcode.BLL.MediatR.Dictionaries.GetById;
using Streetcode.BLL.MediatR.Dictionaries.Update;

namespace Streetcode.WebApi.Controllers.Dictionaries
{
    public class DictionaryController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DictionaryItemDto dictionaryItem)
        {
            return HandleResult(await Mediator.Send(new CreateDictionaryItemCommand(dictionaryItem)));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteDictionaryItemCommand(id)));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetDictionaryItemByIdQuery(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllDictionaryItemsQuery()));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DictionaryItemDto dictionaryItem)
        {
            return HandleResult(await Mediator.Send(new UpdateDictionaryItemCommand(dictionaryItem)));
        }
    }
}
