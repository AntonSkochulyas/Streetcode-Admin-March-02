using Streetcode.DAL.Entities.Dictionaries;
using Streetcode.DAL.Persistence;
using Streetcode.DAL.Repositories.Interfaces.Dictionaries;
using Streetcode.DAL.Repositories.Realizations.Base;

namespace Streetcode.DAL.Repositories.Realizations.Dictionaries
{
    public class DictionaryItemRepository : RepositoryBase<DictionaryItem>, IDictionaryItemRepository
    {
        public DictionaryItemRepository(StreetcodeDbContext dbContext)
        : base(dbContext)
        {
        }
    }
}
