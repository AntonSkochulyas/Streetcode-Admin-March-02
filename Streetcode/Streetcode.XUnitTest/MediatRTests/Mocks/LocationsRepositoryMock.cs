namespace Streetcode.XUnitTest.MediatRTests.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Locations;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    public static Mock<IRepositoryWrapper> GetLocationsRepositoryMock()
    {
        var mockRepo = new Mock<IRepositoryWrapper>();

        var locations = new List<Location>()
            {
                new Location { Id = 1, Streetname = "First StreetName", TableNumber = 1 },
                new Location { Id = 2, Streetname = "First StreetName", TableNumber = 2 },
                new Location { Id = 3, Streetname = "First StreetName", TableNumber = 3 },
                new Location { Id = 4, Streetname = "First StreetName", TableNumber = 4 },
            };

        mockRepo.Setup(x => x.LocationRepository.GetAllAsync(It.IsAny<Expression<Func<Location, bool>>>(), It.IsAny<Func<IQueryable<Location>, IIncludableQueryable<Location, object>>>()))
            .ReturnsAsync(locations);

        mockRepo.Setup(x => x.LocationRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Location, bool>>>(), It.IsAny<Func<IQueryable<Location>, IIncludableQueryable<Location, object>>>()))
            .ReturnsAsync((Expression<Func<Location, bool>> predicate, Func<IQueryable<Location>, IIncludableQueryable<Location, object>> include) =>
            {
                return locations.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.LocationRepository.Create(It.IsAny<Location>()))
            .Returns(locations[0]);

        mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        return mockRepo;
    }
}
