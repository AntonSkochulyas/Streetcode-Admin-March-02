using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Sources.StreetcodeCategoryContent
{
    public class GetByStreetcodeIdStreetcodeCategoryContentSpec : Specification<Entities.Sources.StreetcodeCategoryContent>, ISingleResultSpecification<Entities.Sources.StreetcodeCategoryContent>
    {
        public GetByStreetcodeIdStreetcodeCategoryContentSpec(int streetcodeId, int sourceLinkCategoryId)
        {
            Query.Where(scc => scc.StreetcodeId == streetcodeId && scc.SourceLinkCategoryId == sourceLinkCategoryId);
        }
    }
}
