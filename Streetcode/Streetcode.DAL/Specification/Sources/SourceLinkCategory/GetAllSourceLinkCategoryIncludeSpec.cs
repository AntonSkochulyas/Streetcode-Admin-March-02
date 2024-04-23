using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Sources.SourceLinkCategory
{
    public class GetAllSourceLinkCategoryIncludeSpec : Specification<Entities.Sources.SourceLinkCategory>
    {
        public GetAllSourceLinkCategoryIncludeSpec()
        {
            Query.Include(slc => slc.Image!);
        }
    }
}
