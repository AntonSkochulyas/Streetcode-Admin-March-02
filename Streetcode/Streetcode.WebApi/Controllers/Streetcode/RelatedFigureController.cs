using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.MediatR.Streetcode.RelatedFigure.Create;
using Streetcode.BLL.MediatR.Streetcode.RelatedFigure.Delete;
using Streetcode.BLL.MediatR.Streetcode.RelatedFigure.GetByStreetcodeId;
using Streetcode.BLL.MediatR.Streetcode.RelatedFigure.GetByTagId;

namespace Streetcode.WebApi.Controllers.Streetcode;

/// <summary>
/// Controller for managing related figures.
/// </summary>
public class RelatedFigureController : BaseApiController
{
    /// <summary>
    /// Retrieves related figures by streetcode ID.
    /// </summary>
    /// <param name="streetcodeId">The ID of the streetcode.</param>
    /// <returns>The related figures associated with the streetcode.</returns>
    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new GetRelatedFigureByStreetcodeIdQuery(streetcodeId)));
    }

    /// <summary>
    /// Retrieves related figures by tag ID.
    /// </summary>
    /// <param name="tagId">The ID of the tag.</param>
    /// <returns>The related figures associated with the tag.</returns>
    [HttpGet("{tagId:int}")]
    public async Task<IActionResult> GetByTagId([FromRoute] int tagId)
    {
        return HandleResult(await Mediator.Send(new GetRelatedFiguresByTagIdQuery(tagId)));
    }

    /// <summary>
    /// Creates a new related figure.
    /// </summary>
    /// <param name="ObserverId">The ID of the observer.</param>
    /// <param name="TargetId">The ID of the target.</param>
    /// <returns>The created figure.</returns>
    [Authorize("Admin")]
    [HttpPost("{ObserverId:int}&{TargetId:int}")]
    public async Task<IActionResult> Create([FromRoute] int ObserverId, int TargetId)
    {
        return HandleResult(await Mediator.Send(new CreateRelatedFigureCommand(ObserverId, TargetId)));
    }

    /// <summary>
    /// Deletes a related figure.
    /// </summary>
    /// <param name="ObserverId">The ID of the observer.</param>
    /// <param name="TargetId">The ID of the target.</param>
    /// <returns>The result of the delete operation.</returns>
    [Authorize("Admin")]
    [HttpDelete("{ObserverId:int}&{TargetId:int}")]
    public async Task<IActionResult> Delete([FromRoute] int ObserverId, int TargetId)
    {
        return HandleResult(await Mediator.Send(new DeleteRelatedFigureCommand(ObserverId, TargetId)));
    }
}
