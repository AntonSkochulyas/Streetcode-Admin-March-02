using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.MediatR.AdditionalContent.Tag.GetAll;
using Streetcode.BLL.MediatR.AdditionalContent.Tag.GetById;
using Streetcode.BLL.MediatR.AdditionalContent.Tag.GetByStreetcodeId;
using Streetcode.BLL.MediatR.AdditionalContent.Tag.GetSortedByStartTitle;

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

    /// <summary>
    /// Get sorted tags by start title.
    /// </summary>
    /// <param name="startsWithTitle">Start Title.</param>
    /// <param name="take">Query Take.</param>
    /// <returns>Tag.</returns>
    [HttpGet("get-titles")]
    public async Task<IActionResult> GetSortedTagsByStartTitle([FromQuery] string? startsWithTitle, [FromQuery] int? take)
    {
        return HandleResult(await Mediator.Send(new GetSortedTagsByStartTitleHandlerQuery(startsWithTitle, take)));
    }
}
