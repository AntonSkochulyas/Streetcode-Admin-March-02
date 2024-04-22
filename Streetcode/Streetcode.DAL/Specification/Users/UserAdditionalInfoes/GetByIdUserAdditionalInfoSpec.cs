using Ardalis.Specification;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.DAL.Specification.Users.UserAdditionalInfoes
{
    public class GetByIdUserAdditionalInfoSpec : Specification<UserAdditionalInfo>, ISingleResultSpecification<UserAdditionalInfo>
    {
        public GetByIdUserAdditionalInfoSpec(int id)
        {
            Query.Where(uad => uad.Id == id);
        }
    }
}
