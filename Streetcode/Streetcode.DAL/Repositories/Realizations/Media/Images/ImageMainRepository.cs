using Repositories.Interfaces;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Persistence;
using Streetcode.DAL.Repositories.Realizations.Base;

namespace Streetcode.DAL.Repositories.Realizations.Media.Images;

public class ImageMainRepository : RepositoryBase<ImageMain>, IImageMainRepository
{
    public ImageMainRepository(StreetcodeDbContext dbContext)
        : base(dbContext)
    {
    }
}