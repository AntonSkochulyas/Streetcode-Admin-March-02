using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.MediatR.Media.StreetcodeArt.GetByStreetcodeId;

namespace Streetcode.WebApi.Controllers.Media.Images;

/// <summary>
/// Controller for handling Streetcode Art related operations.
/// </summary>
public class StreetcodeArtController : BaseApiController
{
    /// <summary>
    /// Retrieves Streetcode Art by Streetcode Id.
    /// </summary>
    /// <param name="streetcodeId">The Streetcode Id.</param>
    /// <returns>The Streetcode Art.</returns>
    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new GetStreetcodeArtByStreetcodeIdQuery(streetcodeId)));
    }
}
