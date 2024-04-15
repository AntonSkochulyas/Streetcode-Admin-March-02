using Streetcode.DAL.Entities.Users;
using Streetcode.DAL.Persistence;
using Streetcode.DAL.Repositories.Interfaces.Users;
using Streetcode.DAL.Repositories.Realizations.Base;

namespace Streetcode.DAL.Repositories.Realizations.Users
{
    public class ApplicationUserRepository : RepositoryBase<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(StreetcodeDbContext context)
            : base(context)
        {
        }
    }
}
