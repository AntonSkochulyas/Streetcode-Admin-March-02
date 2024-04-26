using Streetcode.DAL.Entities.Users;
using Streetcode.DAL.Persistence;
using Streetcode.DAL.Repositories.Interfaces.Users;
using Streetcode.DAL.Repositories.Realizations.Base;

namespace Streetcode.DAL.Repositories.Realizations.Users
{
    internal class UserAdditionalInfoRepository : RepositoryBase<UserAdditionalInfo>, IUserAdditionalInfoRepository
    {
        public UserAdditionalInfoRepository(StreetcodeDbContext context)
            : base(context)
        {
        }
    }
}
