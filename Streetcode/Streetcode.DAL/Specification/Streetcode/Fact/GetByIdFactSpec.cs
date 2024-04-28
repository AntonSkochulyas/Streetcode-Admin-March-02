using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Streetcode.Fact
{
    public class GetByIdFactSpec : Specification<Entities.Streetcode.TextContent.Fact>, ISingleResultSpecification<Entities.Streetcode.TextContent.Fact>
    {
        public GetByIdFactSpec(int id)
        {
            Id = id;
            Query.Where(f => f.Id == id);
        }

        public int Id { get; set; }
    }
}
