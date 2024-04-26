using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Streetcode.RelatedFigure
{
    public class GetAllPublishedRelatedFigureSpec : Specification<Entities.Streetcode.StreetcodeContent>
    {
        public GetAllPublishedRelatedFigureSpec(IQueryable<int> relatedFigureIds)
        {
            Query.Where(sc => relatedFigureIds.Any(rfId => rfId == sc.Id) && sc.Status == DAL.Enums.StreetcodeStatus.Published)
                .Include(sc => sc.Images)
                .ThenInclude(img => img.ImageDetails)
                .Include(sc => sc.Tags);
        }
    }
}
