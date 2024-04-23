using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Timeline.TimelineItem
{
    public class GetByStreetcodeIdTimelineItemSpec : Specification<Entities.Timeline.TimelineItem>, ISingleResultSpecification<Entities.Timeline.TimelineItem>
    {
        public GetByStreetcodeIdTimelineItemSpec(int streetcodeId)
        {
            Query.Where(t => t.StreetcodeId == streetcodeId);
        }
    }
}
