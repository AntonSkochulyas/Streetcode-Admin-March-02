using Ardalis.Specification;
using Streetcode.DAL.Entities.Toponyms;

namespace Streetcode.DAL.Specification.Toponyms
{
    public class GetByStreetcodeIdToponymSpec : Specification<Toponym>, ISingleResultSpecification<Toponym>
    {
        public GetByStreetcodeIdToponymSpec(int streetcodeId)
        {
            Query.Where(sc => sc.Streetcodes.Any(s => s.Id == streetcodeId))
                 .Include(sc => sc.Coordinate);
        }
    }
}
