using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Streetcode.RelatedFigure
{
    public class GetByObserverAndTargetIdRelatedFigureSpec : Specification<Entities.Streetcode.RelatedFigure>, ISingleResultSpecification<Entities.Streetcode.RelatedFigure>
    {
        public GetByObserverAndTargetIdRelatedFigureSpec(int observerId, int targetId)
        {
            Query.Where(rf => rf.ObserverId == observerId && rf.TargetId == targetId);
        }
    }
}
