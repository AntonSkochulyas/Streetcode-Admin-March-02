using FluentResults;
using MediatR;

namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Delete
{
    public sealed record DeleteTimelineItemCommand(int Id)
        : IRequest<Result<Unit>>;
}
