using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Streetcode.Fact
{
    public class GetByStreetcodeIdAndOrderNumberFactSpec : Specification<Entities.Streetcode.TextContent.Fact>, ISpecification<Entities.Streetcode.TextContent.Fact>
    {
        public GetByStreetcodeIdAndOrderNumberFactSpec(int streetcodeId, int orderNumber)
        {
            Query.Where(f => f.StreetcodeId == streetcodeId && f.OrderNumber == orderNumber);
        }
    }
}
