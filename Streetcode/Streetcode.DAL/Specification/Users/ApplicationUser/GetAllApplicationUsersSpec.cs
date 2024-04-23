using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Users.ApplicationUser
{
    public class GetAllApplicationUsersSpec : Specification<Entities.Users.ApplicationUser>
    {
        public GetAllApplicationUsersSpec()
        {
            Query.Include(au => au.UserAdditionalInfo);
        }
    }
}
