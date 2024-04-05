using FluentAssertions;
using Moq;
using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;
using Streetcode.XUnitTest.Repositories.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.Repositories.AdditionalContent.Coordinate
{
    public class CoordinateRepositoryTest
    {
        [Fact]
        public void Repository_Create_StreetcodeCoordinate_EqualFirstNames()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetStreetcodeCoordinateRepositoryMock();
            var repository = mockRepo.Object.StreetcodeCoordinateRepository;
            var streetcodeCoordinateToAdd = new StreetcodeCoordinate()
            {
                Id = 4,
                StreetcodeId = 1
            };

            // Act
            var createdStreetcodeCoordinate = repository.Create(streetcodeCoordinateToAdd);

            // Assert
            createdStreetcodeCoordinate.Should().BeEquivalentTo(streetcodeCoordinateToAdd);
        }

        [Fact]
        public async Task Repository_GetAllStreetcodeCoordinates_ReturnsAllStreetcodeCoordinates()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetStreetcodeCoordinateRepositoryMock();
            var repository = mockRepo.Object.StreetcodeCoordinateRepository;

            // Act
            var result = await repository.GetAllAsync(null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public void Repository_StreetcodeCoordinateUpdate()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetStreetcodeCoordinateRepositoryMock();
            var repository = mockRepo.Object.StreetcodeCoordinateRepository;
            var streetcodeCoordinateToUpdate = new StreetcodeCoordinate()
            {
                Id = 1,
                StreetcodeId = 1,
                Longtitude = 1,
                Latitude = 1,
            };

            // Act
            var updatedStreetcodeCoordinate = repository.Update(streetcodeCoordinateToUpdate);

            // Assert
            mockRepo.Verify(x => x.StreetcodeCoordinateRepository.Update(It.IsAny<StreetcodeCoordinate>()), Times.Once);
        }

        [Fact]
        public async Task Repository_DeleteStreetcodeCoordinate_RemovesFromDatabase()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetStreetcodeCoordinateRepositoryMock();
            var repository = mockRepo.Object.StreetcodeCoordinateRepository;
            var streetcodeCoordinateIdToDelete = 1;

            // Act
            repository.Delete(new StreetcodeCoordinate { Id = streetcodeCoordinateIdToDelete });

            // Assert
            var deletedStreetcodeCoordinate = await repository.GetFirstOrDefaultAsync(u => u.Id == streetcodeCoordinateIdToDelete);
            deletedStreetcodeCoordinate.Should().BeNull();
        }
    }
}
