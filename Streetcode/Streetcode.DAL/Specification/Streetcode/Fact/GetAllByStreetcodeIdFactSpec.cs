using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Streetcode.Fact
{
    public class GetAllByStreetcodeIdFactSpec : Specification<Entities.Streetcode.TextContent.Fact>
    {
        public GetAllByStreetcodeIdFactSpec(int streetcodeId)
        {
            Query.Where(f => f.StreetcodeId == streetcodeId);
        }
    }
}
