using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Timeline;

namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Delete
{
    public sealed record DeleteTimelineItemCommand(int Id)
        : IRequest<Result<TimelineItemDto>>;
}
