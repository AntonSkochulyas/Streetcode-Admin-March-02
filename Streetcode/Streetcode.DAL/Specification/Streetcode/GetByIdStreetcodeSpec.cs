using Ardalis.Specification;
using Streetcode.DAL.Entities.Streetcode;

namespace Streetcode.DAL.Specification.Team
{
    public class GetByIdStreetcodeSpec : Specification<StreetcodeContent>, ISingleResultSpecification<StreetcodeContent>
    {
        public GetByIdStreetcodeSpec(int id)
        {
            Query.Where(s => s.Id == id);
        }
    }
}
