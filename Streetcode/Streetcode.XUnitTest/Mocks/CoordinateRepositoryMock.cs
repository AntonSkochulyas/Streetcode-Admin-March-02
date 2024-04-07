namespace Streetcode.XUnitTest.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    public static Mock<IRepositoryWrapper> GetCoordinateRepositoryMock()
    {
        var coordinate = new StreetcodeCoordinate()
        {
            Id = 1,
            StreetcodeId = 1,
        };

        List<StreetcodeCoordinate> coordinates = new List<StreetcodeCoordinate>
        {
            new StreetcodeCoordinate
            {
                Id = 1,
                StreetcodeId = 1,
            },
            new StreetcodeCoordinate
            {
                Id = 2,
                StreetcodeId = 1,
            },
        };

        var mockRepo = new Mock<IRepositoryWrapper>();

        mockRepo.Setup(x => x.StreetcodeCoordinateRepository.Create(It.IsAny<StreetcodeCoordinate>()))
            .Returns(coordinate);

        mockRepo.Setup(x => x.StreetcodeCoordinateRepository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<StreetcodeCoordinate, bool>>>(),
            It.IsAny<Func<IQueryable<StreetcodeCoordinate>, IIncludableQueryable<StreetcodeCoordinate, object>>>()))
            .ReturnsAsync((Expression<Func<StreetcodeCoordinate, bool>> predicate, Func<IQueryable<StreetcodeCoordinate>,
            IIncludableQueryable<StreetcodeCoordinate, object>> include) =>
            {
                return coordinates.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.StreetcodeCoordinateRepository.GetAllAsync(
            It.IsAny<Expression<Func<StreetcodeCoordinate, bool>>>(),
            It.IsAny<Func<IQueryable<StreetcodeCoordinate>, IIncludableQueryable<StreetcodeCoordinate, object>>>()))
            .ReturnsAsync(coordinates);

        mockRepo.Setup(repo => repo.StreetcodeCoordinateRepository.Update(It.IsAny<StreetcodeCoordinate>()));

        mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        return mockRepo;
    }
}
