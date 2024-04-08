using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.MediatR.AdditionalContent.Tag.GetAll;
using Streetcode.BLL.MediatR.AdditionalContent.Tag.GetById;
using Streetcode.BLL.MediatR.AdditionalContent.Tag.GetByStreetcodeId;

namespace Streetcode.WebApi.Controllers.AdditionalContent;

/// <summary>
/// Controller with CRUD operations for Tag entity.
/// </summary>
public class TagController : BaseApiController
{
    /// <summary>
    /// Get all tags.
    /// </summary>
    /// <returns>Tags.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await Mediator.Send(new GetAllTagsQuery()));
    }

    /// <summary>
    /// Get tag by id.
    /// </summary>
    /// <param name="id">The id of the tag to find.</param>
    /// <returns>Tag.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetTagByIdQuery(id)));
    }

    /// <summary>
    /// Get tag by streetcode id.
    /// </summary>
    /// <param name="streetcodeId">The id of the streetcode to find.</param>
    /// <returns>Tags.</returns>
    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new GetTagByStreetcodeIdQuery(streetcodeId)));
    }

    /// <summary>
    /// Get tag by title.
    /// </summary>
    /// <param name="title">The title of the tag to find.</param>
    /// <returns>Tag.</returns>
    [HttpGet("{title}")]
    public async Task<IActionResult> GetTagByTitle([FromRoute] string title)
    {
        return HandleResult(await Mediator.Send(new GetTagByTitleQuery(title)));
    }

    [HttpGet("startsWith/{startTitle}/take/{take}")]
    public async Task<IActionResult> GetTagByTitle([FromRoute] string startTitle, int take = 10)
    {
        return HandleResult(await Mediator.Send(new GetSortedTagsByStartTitleHandlerQuery(take, startTitle)));
    }
}
