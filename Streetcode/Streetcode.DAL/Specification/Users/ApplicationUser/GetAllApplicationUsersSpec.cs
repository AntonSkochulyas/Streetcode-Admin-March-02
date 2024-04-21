using Ardalis.Specification;

namespace Streetcode.DAL.Specification.Users.ApplicationUser
{
    public class GetAllApplicationUsersSpec : Specification<Streetcode.DAL.Entities.Users.ApplicationUser>
    {
        public GetAllApplicationUsersSpec()
        {
            Query.Include(au => au.UserAdditionalInfo);
        }
    }
}
