using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Timeline.TimelineItem
{
    public class GetByIdTimelineItemIncludeSpec : Specification<Streetcode.DAL.Entities.Timeline.TimelineItem>, ISingleResultSpecification<Streetcode.DAL.Entities.Timeline.TimelineItem>
    {
        public GetByIdTimelineItemIncludeSpec(int id)
        {
            Query.Where(t => t.Id == id).Include(htc => htc.HistoricalContextTimelines).ThenInclude(hc => hc.HistoricalContext);
        }
    }
}
