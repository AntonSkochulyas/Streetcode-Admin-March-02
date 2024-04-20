// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Timeline;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Delete
{
    /// <summary>
    /// Query, that requests a handler to delete a timeline item by given id.
    /// </summary>
    /// <param name="id">
    /// Timeline item id to delete.
    /// </param>
    public sealed record DeleteTimelineItemCommand(int Id)
        : IRequest<Result<TimelineItemDto>>;
}
