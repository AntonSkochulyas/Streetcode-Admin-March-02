using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Sources.StreetcodeCategoryContent
{
    public class GetByStreetcodeIdStreetcodeCategoryContentSpec : Specification<Entities.Sources.StreetcodeCategoryContent>, ISingleResultSpecification<Entities.Sources.StreetcodeCategoryContent>
    {
        public GetByStreetcodeIdStreetcodeCategoryContentSpec(int streetcodeId, int sourceLinkCategoryId)
        {
            StreetcodeId = streetcodeId;
            SourceLinkId = sourceLinkCategoryId;
            Query.Where(scc => scc.StreetcodeId == streetcodeId && scc.SourceLinkCategoryId == sourceLinkCategoryId);
        }

        public int StreetcodeId { get; set; }
        public int SourceLinkId { get; set; }
    }
}
