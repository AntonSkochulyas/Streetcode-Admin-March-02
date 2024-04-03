using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Create;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Delete;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.GetAll;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.GetById;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Update;

namespace Streetcode.WebApi.Controllers.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks
{
    public class AuthorHyperLinkController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuthorShipHyperLinkDto authorHyperLink)
        {
            return HandleResult(await Mediator.Send(new CreateAuthorShipHyperLinkCommand(authorHyperLink)));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteAuthorShipHyperLinkCommand(id)));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new GetAuthorShipHyperLinksByIdQuery(id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return HandleResult(await Mediator.Send(new GetAllAuthorShipHyperLinksQuery()));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AuthorShipHyperLinkDto authorHyperLink)
        {
            return HandleResult(await Mediator.Send(new UpdateAuthorShipHyperLinkCommand(authorHyperLink)));
        }
    }
}
