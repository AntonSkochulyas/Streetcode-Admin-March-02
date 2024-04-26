// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Timeline;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Create
{
    /// <summary>
    /// Command, that requests a handler to create a new timeline item.
    /// </summary>
    /// <param name="newTimelineItem">
    /// New timeline item.
    /// </param>
    public record CreateTimelineItemCommand(TimelineItemCreateDto TimelineItem)
        : IRequest<Result<TimelineItemDto>>;
}
