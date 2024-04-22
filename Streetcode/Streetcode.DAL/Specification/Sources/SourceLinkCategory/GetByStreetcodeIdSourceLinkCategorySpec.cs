using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Sources.SourceLinkCategory
{
    public class GetByStreetcodeIdSourceLinkCategorySpec : Specification<Entities.Sources.SourceLinkCategory>
    {
        public GetByStreetcodeIdSourceLinkCategorySpec(int streetcodeId)
        {
            Query.Where(slc => slc.Id == streetcodeId).Include(i => i.Image);
        }
    }
}
