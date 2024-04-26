using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;
using Streetcode.DAL.Persistence;
using Streetcode.DAL.Repositories.Interfaces.InfoBlocks.AuthorsInfoes;
using Streetcode.DAL.Repositories.Realizations.Base;

namespace Streetcode.DAL.Repositories.Realizations.InfoBlocks.AuthorsInfoes
{
    public class AuthorShipRepository : RepositoryBase<AuthorShip>, IAuthorShipRepository
    {
        public AuthorShipRepository(StreetcodeDbContext dbContext)
        : base(dbContext)
        {
        }
    }
}
