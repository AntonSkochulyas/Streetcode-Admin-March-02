using FluentAssertions;
using Moq;
using Streetcode.DAL.Entities.Toponyms;
<<<<<<< HEAD:Streetcode/Streetcode.XUnitTest/Repositories/Toponyms/ToponymsRepositoryTest.cs
using Streetcode.XUnitTest.Repositories.Mocks;
=======
>>>>>>> develop:Streetcode/Streetcode.XUnitTest/Repositories/Users/ToponymsRepositoryTest.cs
using Xunit;
using Streetcode.XUnitTest.Mocks;

<<<<<<< HEAD:Streetcode/Streetcode.XUnitTest/Repositories/Toponyms/ToponymsRepositoryTest.cs
namespace Streetcode.XUnitTest.Repositories.Toponyms
=======
namespace Streetcode.XUnitTest.Repositories.Users
>>>>>>> develop:Streetcode/Streetcode.XUnitTest/Repositories/Users/ToponymsRepositoryTest.cs
{
    public class ToponymsRepositoryTest
    {
        [Fact]
        public async Task Repository_CreateToponym()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetToponymsRepositoryMock();
            var repository = mockRepo.Object.ToponymRepository;
            var newToponym = new Toponym() { Id = 4, Community = "Fourth community", AdminRegionNew = "Fourth region new", AdminRegionOld = "Fourth region old" };

            // Act
            var createdToponym = repository.Create(newToponym);

            // Assert
            Assert.NotNull(createdToponym);
            Assert.Equal(newToponym, createdToponym);
        }

        [Fact]
        public async Task Repository_GetAllToponyms_ReturnsAllToponyms()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetToponymsRepositoryMock();

            // Act
            var resultTask = mockRepo.Object.ToponymRepository.GetAllAsync(null, null);
            var result = resultTask.Result;

            // Assert
            result.Count().Should().Be(4);
            result.Should().ContainSingle(u => u.Community == "First community")
                  .And.ContainSingle(u => u.Community == "Second community")
                  .And.ContainSingle(u => u.Community == "Third community")
                  .And.ContainSingle(u => u.Community == "Fourth community");
        }

        [Fact]
        public async Task Repository_DeleteToponym()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetToponymsRepositoryMock();
            var repository = mockRepo.Object.ToponymRepository;
            var toponymIdToDelete = 1;

            // Act
            repository.Delete(new Toponym { Id = toponymIdToDelete });

            // Assert
            mockRepo.Verify(x => x.ToponymRepository.Delete(It.Is<Toponym>(u => u.Id == toponymIdToDelete)), Times.Once);
        }

        [Fact]
        public async Task Repository_DeleteToponym_RemovesFromDatabase()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetToponymsRepositoryMock();
            var repository = mockRepo.Object.ToponymRepository;
            var toponymIdToDelete = 1;

            // Act
            repository.Delete(new Toponym { Id = toponymIdToDelete });

            // Assert
            var deletedUser = await repository.GetFirstOrDefaultAsync(u => u.Id == toponymIdToDelete);
            Assert.Null(deletedUser);
        }

        [Fact]
        public async Task Repository_ToponymUpdate()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetToponymsRepositoryMock();
            var repository = mockRepo.Object.ToponymRepository;
            var toponymToUpdate = new Toponym { Id = 1, Community = "Updated first community", AdminRegionNew = "Updated first region new", AdminRegionOld = "Updated first region old" };

            // Act
            var updatedUser = repository.Update(toponymToUpdate);

            // Assert
            mockRepo.Verify(x => x.ToponymRepository.Update(It.IsAny<Toponym>()), Times.Once);
        }
    }
}
