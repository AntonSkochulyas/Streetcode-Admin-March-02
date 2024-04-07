// Necessary usings
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.AdditionalContent.Coordinates.Types;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Delete;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.GetByStreetcodeId;

// Necessary namespaces
namespace Streetcode.WebApi.Controllers.AdditionalContent;

/// <summary>
/// Controller that handles endpoints for:
/// - Create coordinate;
/// - Delete coordinate by id;
/// - Get coordinate by streetcode id.
/// </summary>
public class CoordinateController : BaseApiController
{
    // Get coordinate by streetcode id
    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new GetCoordinatesByStreetcodeIdQuery(streetcodeId)));
    }

    // Create coordinate
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StreetcodeCoordinateDto coordinateDto)
    {
        return HandleResult(await Mediator.Send(new CreateCoordinateCommand(coordinateDto)));
    }

    // Delete coordinate by id
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new DeleteCoordinateCommand(id)));
    }
}