using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Sources.SourceLinkCategory
{
    public class GetByIdSourceLinkCategorySpec : Specification<Entities.Sources.SourceLinkCategory>, ISingleResultSpecification<Entities.Sources.SourceLinkCategory>
    {
        public GetByIdSourceLinkCategorySpec(int id)
        {
            Query.Where(slc => slc.Id == id);
        }
    }
}
