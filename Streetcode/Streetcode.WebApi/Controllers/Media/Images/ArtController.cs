using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.MediatR.Media.Art.GetAll;
using Streetcode.BLL.MediatR.Media.Art.GetById;
using Streetcode.BLL.MediatR.Media.Art.GetByStreetcodeId;

namespace Streetcode.WebApi.Controllers.Media.Images;

/// <summary>
/// Controller for managing art images.
/// </summary>
public class ArtController : BaseApiController
{
    /// <summary>
    /// Get all art images.
    /// </summary>
    /// <returns>A list of art images.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await Mediator.Send(new GetAllArtsQuery()));
    }

    /// <summary>
    /// Get an art image by its ID.
    /// </summary>
    /// <param name="id">The ID of the art image.</param>
    /// <returns>The art image with the specified ID.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetArtByIdQuery(id)));
    }

    /// <summary>
    /// Get art images by streetcode ID.
    /// </summary>
    /// <param name="streetcodeId">The streetcode ID.</param>
    /// <returns>A list of art images associated with the specified streetcode ID.</returns>
    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new GetArtsByStreetcodeIdQuery(streetcodeId)));
    }
}
