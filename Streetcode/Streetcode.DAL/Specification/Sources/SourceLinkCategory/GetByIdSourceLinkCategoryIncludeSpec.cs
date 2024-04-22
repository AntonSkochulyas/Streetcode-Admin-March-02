using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Sources.SourceLinkCategory
{
    public class GetByIdSourceLinkCategoryIncludeSpec : Specification<Entities.Sources.SourceLinkCategory>, ISingleResultSpecification<Entities.Sources.SourceLinkCategory>
    {
        public GetByIdSourceLinkCategoryIncludeSpec(int id)
        {
            Query.Where(scl => scl.Id == id).Include(scc => scc.StreetcodeCategoryContents).Include(i => i.Image!);
        }
    }
}
