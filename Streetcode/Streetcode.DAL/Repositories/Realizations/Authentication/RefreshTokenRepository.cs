using Streetcode.DAL.Entities.Authentication;
using Streetcode.DAL.Persistence;
using Streetcode.DAL.Repositories.Interfaces.Authentication;
using Streetcode.DAL.Repositories.Realizations.Base;

namespace Streetcode.DAL.Repositories.Realizations.Authentication;

public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(StreetcodeDbContext dbContext)
        : base(dbContext)
    {
    }
}