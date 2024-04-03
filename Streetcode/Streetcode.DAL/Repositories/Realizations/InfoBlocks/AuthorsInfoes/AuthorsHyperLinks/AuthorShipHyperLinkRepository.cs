using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.DAL.Persistence;
using Streetcode.DAL.Repositories.Interfaces.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.DAL.Repositories.Realizations.Base;

namespace Streetcode.DAL.Repositories.Realizations.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks
{
    public class AuthorShipHyperLinkRepository : RepositoryBase<AuthorShipHyperLink>, IAuthorHyperLinkRepository
    {
        public AuthorShipHyperLinkRepository(StreetcodeDbContext dbContext)
        : base(dbContext)
        {
        }
    }
}
