namespace Streetcode.XUnitTest.MediatRTests.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    /// <summary>
    /// Mocks streetcode art repository.
    /// </summary>
    /// <returns>Returns mocked repository. </returns>
    public static Mock<IRepositoryWrapper> GetStreetcodeArtRepositoryMock()
    {
        var streetcodeArts = new List<StreetcodeArt>
            {
                new StreetcodeArt() { Index = 1, StreetcodeId = 1, ArtId = 1 },
                new StreetcodeArt() { Index = 2, StreetcodeId = 2, ArtId = 2 },
                new StreetcodeArt() { Index = 3, StreetcodeId = 3, ArtId = 3 },
                new StreetcodeArt() { Index = 4, StreetcodeId = 4, ArtId = 4 },
            };

        var mockRepo = new Mock<IRepositoryWrapper>();

        mockRepo.Setup(x => x.StreetcodeArtRepository.GetAllAsync(It.IsAny<Expression<Func<StreetcodeArt, bool>>>(), It.IsAny<Func<IQueryable<StreetcodeArt>, IIncludableQueryable<StreetcodeArt, object>>>()))
            .ReturnsAsync(streetcodeArts);

        mockRepo.Setup(x => x.StreetcodeArtRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<StreetcodeArt, bool>>>(), It.IsAny<Func<IQueryable<StreetcodeArt>, IIncludableQueryable<StreetcodeArt, object>>>()))
            .ReturnsAsync((Expression<Func<StreetcodeArt, bool>> predicate, Func<IQueryable<StreetcodeArt>, IIncludableQueryable<StreetcodeArt, object>> include) =>
            {
                return streetcodeArts.FirstOrDefault(predicate.Compile());
            });

        return mockRepo;
    }
}
