using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.Dto.Timeline;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.Create;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.Delete;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.GetAll;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.GetById;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.GetByStreetcodeId;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.Update;

namespace Streetcode.WebApi.Controllers.Timeline;

/// <summary>
/// Represents a controller for managing timeline items.
/// </summary>
public class TimelineItemController : BaseApiController
{
    /// <summary>
    /// Retrieves all timeline items.
    /// </summary>
    /// <returns>The collection of timeline items.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await Mediator.Send(new GetAllTimelineItemsQuery()));
    }

    /// <summary>
    /// Retrieves a timeline item by its ID.
    /// </summary>
    /// <param name="id">The ID of the timeline item.</param>
    /// <returns>The timeline item with the specified ID.</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetTimelineItemByIdQuery(id)));
    }

    /// <summary>
    /// Retrieves timeline items by streetcode ID.
    /// </summary>
    /// <param name="streetcodeId">The ID of the streetcode.</param>
    /// <returns>The collection of timeline items associated with the specified streetcode ID.</returns>
    [HttpGet("{streetcodeId:int}")]
    public async Task<IActionResult> GetByStreetcodeId([FromRoute] int streetcodeId)
    {
        return HandleResult(await Mediator.Send(new GetTimelineItemsByStreetcodeIdQuery(streetcodeId)));
    }

    /// <summary>
    /// Creates a new timeline item.
    /// </summary>
    /// <param name="timelineItem">The timeline item to create.</param>
    /// <returns>The created timeline item.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TimelineItemDto timelineItem)
    {
        return HandleResult(await Mediator.Send(new CreateTimelineItemCommand(timelineItem)));
    }

    /// <summary>
    /// Updates an existing timeline item.
    /// </summary>
    /// <param name="timelineItem">The timeline item to update.</param>
    /// <returns>The updated timeline item.</returns>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TimelineItemDto timelineItem)
    {
        return HandleResult(await Mediator.Send(new UpdateTimelineItemCommand(timelineItem)));
    }

    /// <summary>
    /// Deletes a timeline item by its ID.
    /// </summary>
    /// <param name="id">The ID of the timeline item to delete.</param>
    /// <returns>The result of the deletion operation.</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new DeleteTimelineItemCommand(id)));
    }
}
