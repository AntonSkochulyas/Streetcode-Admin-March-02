using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.GetByStreetcodeId;

namespace Streetcode.WebApi.Controllers.AdditionalContent;

/// <summary>
/// Controller with CRUD operations for Coordinate entity.
/// </summary>
public class CoordinateController : BaseApiController
{
    /// <summary>
    /// Get coordinate by streetcode id.
    /// </summary>
    /// <param name="streetcodeId">The id of the streetcode to find.</param>
    /// <returns>Coordinates.</returns>
    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new GetCoordinatesByStreetcodeIdQuery(streetcodeId)));
    }
}