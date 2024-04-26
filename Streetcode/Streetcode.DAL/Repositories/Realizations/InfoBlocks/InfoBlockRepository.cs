using Streetcode.DAL.Entities.InfoBlocks;
using Streetcode.DAL.Persistence;
using Streetcode.DAL.Repositories.Interfaces.InfoBlocks;
using Streetcode.DAL.Repositories.Realizations.Base;

namespace Streetcode.DAL.Repositories.Realizations.InfoBlocks
{
    public class InfoBlockRepository : RepositoryBase<InfoBlock>, IInfoBlockRepository
    {
        public InfoBlockRepository(StreetcodeDbContext dbContext)
        : base(dbContext)
        {
        }
    }
}
