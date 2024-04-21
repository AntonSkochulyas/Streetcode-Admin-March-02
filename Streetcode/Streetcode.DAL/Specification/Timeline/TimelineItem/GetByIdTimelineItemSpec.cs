using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Timeline.TimelineItem
{
    public class GetByIdTimelineItemSpec : Specification<Streetcode.DAL.Entities.Timeline.TimelineItem>, ISingleResultSpecification<Streetcode.DAL.Entities.Timeline.TimelineItem>
    {
        public GetByIdTimelineItemSpec(int id)
        {
            Query.Where(t => t.Id == id);
        }
    }
}
