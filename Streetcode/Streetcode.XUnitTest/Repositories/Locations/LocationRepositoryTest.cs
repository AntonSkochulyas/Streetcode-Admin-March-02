using FluentAssertions;
using Moq;
using Streetcode.DAL.Entities.Locations;
using Streetcode.XUnitTest.MediatRTests.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.Repositories.Locations;

public class LocationRepositoryTest
{
    [Fact]
    public async Task Repository_Create_Location_EqualStreetname()
    {
        // Arrange
        var mockRepo = RepositoryMocker.GetLocationRepositoryMock();
        var repository = mockRepo.Object.LocationRepository;

        var locationToAdd = new Location { Id = 5, Streetname = "5 StreetName", TableNumber = 5 };

        // Act
        var createdLocation = repository.Create(locationToAdd);

        // Assert
        createdLocation.Streetname.Should().Be(locationToAdd.Streetname);
    }

    [Fact]
    public async Task Repository_GetAllLocations_ReturnsAllLocations()
    {
        // Arrange
        var mockRepo = RepositoryMocker.GetLocationRepositoryMock();
        var repository = mockRepo.Object.LocationRepository;

        // Act
        var result = await repository.GetAllAsync(null, null);

        // Assert
        result.Count().Should().Be(4);
    }

    [Fact]
    public async Task Repository_SPartnerUpdate()
    {
        // Arrange
        var mockRepo = RepositoryMocker.GetLocationRepositoryMock();
        var repository = mockRepo.Object.LocationRepository;
        var locationToUpdate = new Location { Id = 1, Streetname = "1st StreetName" };

        // Act
        var updatedLocation = repository.Update(locationToUpdate);

        // Assert
        mockRepo.Verify(x => x.LocationRepository.Update(It.IsAny<Location>()), Times.Once);
    }

    [Fact]
    public async Task Repository_DeleteLocation_RemovesFromDatabase()
    {
        // Arrange
        var mockRepo = RepositoryMocker.GetLocationRepositoryMock();
        var repository = mockRepo.Object.LocationRepository;
        var locationIdToDelete = 1;

        // Act
        repository.Delete(new Location { Id = locationIdToDelete });

        // Assert
        var deletedPartner = await repository.GetFirstOrDefaultAsync(p => p.Id == locationIdToDelete);
        deletedPartner.Should().BeNull();
    }
}
