using Streetcode.DAL.Entities.InfoBlocks.Articles;
using Streetcode.DAL.Persistence;
using Streetcode.DAL.Repositories.Interfaces.InfoBlocks.Articles;
using Streetcode.DAL.Repositories.Realizations.Base;

namespace Streetcode.DAL.Repositories.Realizations.InfoBlocks.Articles
{
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(StreetcodeDbContext dbContext)
        : base(dbContext)
        {
        }
    }
}
