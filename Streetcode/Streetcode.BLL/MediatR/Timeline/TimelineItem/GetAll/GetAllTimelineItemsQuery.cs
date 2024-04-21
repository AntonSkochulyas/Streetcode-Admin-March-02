// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Timeline;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.GetAll
{
    /// <summary>
    /// Query, that requests a handler to get all timelineitems from database.
    /// </summary>
    public record GetAllTimelineItemsQuery : IRequest<Result<IEnumerable<TimelineItemDto>>>
    {
        public GetAllTimelineItemsQuery()
        {
        }
    }
}