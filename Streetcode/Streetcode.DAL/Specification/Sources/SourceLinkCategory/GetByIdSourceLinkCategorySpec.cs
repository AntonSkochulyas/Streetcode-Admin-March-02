using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Sources.SourceLinkCategory
{
    public class GetByIdSourceLinkCategorySpec : Specification<Entities.Sources.SourceLinkCategory>, ISingleResultSpecification<Entities.Sources.SourceLinkCategory>
    {
        public int Id;
        public GetByIdSourceLinkCategorySpec(int id)
        {
            Id = id;
            Query.Where(slc => slc.Id == id);
        }
    }
}
