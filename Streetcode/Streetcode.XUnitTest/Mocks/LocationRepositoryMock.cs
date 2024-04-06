namespace Streetcode.XUnitTest.MediatRTests.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Locations;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    public static Mock<IRepositoryWrapper> GetLocationRepositoryMock()
    {
        var mockRepo = new Mock<IRepositoryWrapper>();

        var locations = new List<Location>()
            {
                new Location { Id = 1, Streetname = "1 StreetName", TableNumber = 1 },
                new Location { Id = 2, Streetname = "2 StreetName", TableNumber = 2 },
                new Location { Id = 3, Streetname = "3 StreetName", TableNumber = 3 },
                new Location { Id = 4, Streetname = "4 StreetName", TableNumber = 4 },
            };

        mockRepo.Setup(x => x.LocationRepository
                .GetAllAsync(
                    It.IsAny<Expression<Func<Location, bool>>>(),
                    It.IsAny<Func<IQueryable<Location>, IIncludableQueryable<Location, object>>>()))
                .ReturnsAsync(locations);

        mockRepo.Setup(x => x.LocationRepository
            .GetFirstOrDefaultAsync(
                It.IsAny<Expression<Func<Location, bool>>>(), 
                It.IsAny<Func<IQueryable<Location>, IIncludableQueryable<Location, object>>>()))
            .ReturnsAsync((Expression<Func<Location, bool>> predicate, Func<IQueryable<Location>, IIncludableQueryable<Location, object>> include) =>
            {
                return locations.FirstOrDefault(predicate.Compile());
            });

        mockRepo.Setup(x => x.LocationRepository.Create(It.IsAny<Location>()))
            .Returns((Location location) =>
            {
                locations.Add(location);
                return location;
            });

        mockRepo.Setup(x => x.LocationRepository.Delete(It.IsAny<Location>()))
            .Callback((Location? location) =>
            {
                location = locations.FirstOrDefault(x => x.Id == location.Id);
                if(location != null)
                {
                    locations.Remove(location);
                }
            });

        mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        return mockRepo;
    }
}
