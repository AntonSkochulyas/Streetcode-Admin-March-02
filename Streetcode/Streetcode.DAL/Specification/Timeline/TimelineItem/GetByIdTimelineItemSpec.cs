using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Timeline.TimelineItem
{
    public class GetByIdTimelineItemSpec : Specification<Entities.Timeline.TimelineItem>, ISingleResultSpecification<Entities.Timeline.TimelineItem>
    {
        public GetByIdTimelineItemSpec(int id)
        {
            Id = id;
            Query.Where(t => t.Id == id);
        }

        public int Id { get; set; }
    }
}
