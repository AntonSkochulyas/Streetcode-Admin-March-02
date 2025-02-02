﻿// Necessary usings
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.AdditionalContent.Coordinates.Types;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Delete;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.GetByStreetcodeId;

// Necessary namespaces
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

    /// <summary>
    /// Create a new coordinate.
    /// </summary>
    /// <param name="createStreetcodeCoordinateDto">The create coordinate DTO.</param>
    /// <returns>The created coordinate.</returns>
    [Authorize("Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStreetcodeCoordinateDto createStreetcodeCoordinateDto)
    {
        return HandleResult(await Mediator.Send(new CreateCoordinateCommand(createStreetcodeCoordinateDto)));
    }

    /// <summary>
    /// Delete a coordinate by id.
    /// </summary>
    /// <param name="id">The id of the coordinate to delete.</param>
    /// <returns>The result of the delete operation.</returns>
    [Authorize("Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new DeleteCoordinateCommand(id)));
    }
}
