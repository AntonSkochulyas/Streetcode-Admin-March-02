using Ardalis.Specification;
using Streetcode.DAL.Entities.Toponyms;

namespace Streetcode.DAL.Specification.Toponyms
{
    public class GetByIdToponymSpec : Specification<Toponym>, ISingleResultSpecification<Toponym>
    {
        public GetByIdToponymSpec(int id)
        {
            Query.Where(t => t.Id == id);
        }
    }
}
