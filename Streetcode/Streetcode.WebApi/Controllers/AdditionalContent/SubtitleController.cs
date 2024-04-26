using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.MediatR.AdditionalContent.GetById;
using Streetcode.BLL.MediatR.AdditionalContent.Subtitle.GetAll;
using Streetcode.BLL.MediatR.AdditionalContent.Subtitle.GetByStreetcodeId;

namespace Streetcode.WebApi.Controllers.AdditionalContent;

/// <summary>
/// Controller with CRUD operations for Subtitle entity.
/// </summary>
public class SubtitleController : BaseApiController
{
    /// <summary>
    /// Get all subtitles.
    /// </summary>
    /// <returns>Subtitles.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await Mediator.Send(new GetAllSubtitlesQuery()));
    }

    /// <summary>
    /// Get subtitle by id.
    /// </summary>
    /// <param name="id">The id of the subtitle to find.</param>
    /// <returns>Subtitles.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetSubtitleByIdQuery(id)));
    }

    /// <summary>
    /// Get subtitle by streetcode id.
    /// </summary>
    /// <param name="streetcodeId">The id of the streetcode to find.</param>
    /// <returns>Subtitles.</returns>
    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new GetSubtitlesByStreetcodeIdQuery(streetcodeId)));
    }
}