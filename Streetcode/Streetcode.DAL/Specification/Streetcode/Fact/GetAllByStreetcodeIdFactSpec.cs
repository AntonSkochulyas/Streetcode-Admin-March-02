using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Streetcode.Fact
{
    public class GetAllByStreetcodeIdFactSpec : Specification<Entities.Streetcode.TextContent.Fact>
    {
        public int StreetcodeId;
        public GetAllByStreetcodeIdFactSpec(int streetcodeId)
        {
            StreetcodeId = streetcodeId;
            Query.Where(f => f.StreetcodeId == streetcodeId);
        }
    }
}
