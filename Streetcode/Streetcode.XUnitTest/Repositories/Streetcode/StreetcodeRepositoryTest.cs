using FluentAssertions;
using Moq;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.Repositories.Streetcode
{
    public class StreetcodeRepositoryTest
    {
        [Fact]
        public async Task Repository_Create_StreetcodeContent_EqualTitles()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetStreetcodeRepositoryMock();
            var repository = mockRepo.Object.StreetcodeRepository;
            var streetcodeToAdd = new StreetcodeContent { Id = 5, Title = "First Streetcode", TransliterationUrl = "first-streetcode", DateString = "2024-04-05" };

            // Act
            var createdStreetcode = repository.Create(streetcodeToAdd);

            // Assert
            createdStreetcode.Title.Should().Be(streetcodeToAdd.Title);
        }

        [Fact]
        public async Task Repository_GetAllStreetcode_ReturnsAllStreetcodes()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetStreetcodeRepositoryMock();
            var repository = mockRepo.Object.StreetcodeRepository;

            // Act
            var result = await repository.GetAllAsync(null, null);

            // Assert
            result.Count().Should().Be(4);
        }

        [Fact]
        public async Task Repository_StreetcodeUpdate()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetStreetcodeRepositoryMock();
            var repository = mockRepo.Object.StreetcodeRepository;
            var streetcodeToUpdate = new StreetcodeContent { Id = 1, Title = "First Streetcode", TransliterationUrl = "first-streetcode", DateString = "2024-04-05", Teaser = "Updated Teaser" };

            // Act
            var updatedStreetcode = repository.Update(streetcodeToUpdate);

            // Assert
            mockRepo.Verify(x => x.StreetcodeRepository.Update(It.IsAny<StreetcodeContent>()), Times.Once);
        }

        [Fact]
        public async Task Repository_DeleteStreetcode_RemovesFromDatabase()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetStreetcodeRepositoryMock();
            var repository = mockRepo.Object.StreetcodeRepository;
            var streetcodeIdToDelete = 1;

            // Act
            repository.Delete(new StreetcodeContent { Id = streetcodeIdToDelete });

            // Assert
            var deletedStreetcode = await repository.GetFirstOrDefaultAsync(u => u.Id == streetcodeIdToDelete);
            deletedStreetcode.Should().BeNull();
        }
    }
}
