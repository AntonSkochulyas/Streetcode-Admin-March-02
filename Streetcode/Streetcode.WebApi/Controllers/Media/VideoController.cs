using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.MediatR.Media.Video.GetAll;
using Streetcode.BLL.MediatR.Media.Video.GetById;
using Streetcode.BLL.MediatR.Media.Video.GetByStreetcodeId;

namespace Streetcode.WebApi.Controllers.Media;

/// <summary>
/// Controller for handling video-related operations.
/// </summary>
public class VideoController : BaseApiController
{
    /// <summary>
    /// Retrieves all videos.
    /// </summary>
    /// <returns>The list of all videos.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await Mediator.Send(new GetAllVideosQuery()));
    }

    /// <summary>
    /// Retrieves a video by streetcode ID.
    /// </summary>
    /// <param name="streetcodeId">The ID of the streetcode.</param>
    /// <returns>The video with the specified streetcode ID.</returns>
    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new GetVideoByStreetcodeIdQuery(streetcodeId)));
    }

    /// <summary>
    /// Retrieves a video by ID.
    /// </summary>
    /// <param name="id">The ID of the video.</param>
    /// <returns>The video with the specified ID.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetVideoByIdQuery(id)));
    }
}
