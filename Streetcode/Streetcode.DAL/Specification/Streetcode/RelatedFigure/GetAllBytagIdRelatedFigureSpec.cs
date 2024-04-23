using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Streetcode.RelatedFigure
{
    public class GetAllBytagIdRelatedFigureSpec : Specification<Entities.Streetcode.StreetcodeContent>
    {
        public GetAllBytagIdRelatedFigureSpec(int tagId)
        {
            Query.Where(sc => sc.Status == DAL.Enums.StreetcodeStatus.Published &&
                      sc.Tags.Select(t => t.Id).Any(tag => tag == tagId))
                .Include(sc => sc.Images)
                .Include(sc => sc.Tags);
        }
    }
}
