using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Timeline.TimelineItem
{
    public class GetAllTimelineItemSpec : Specification<Streetcode.DAL.Entities.Timeline.TimelineItem>
    {
        public GetAllTimelineItemSpec()
        {
            Query.Include(hct => hct.HistoricalContextTimelines).ThenInclude(ht => ht.HistoricalContext);
        }
    }
}
