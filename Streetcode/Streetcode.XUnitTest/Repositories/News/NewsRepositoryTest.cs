using FluentAssertions;
using Moq;
using Streetcode.DAL.Entities.News;
using Streetcode.XUnitTest.Repositories.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.Repositories.News
{
    public class NewsRepositoryTest
    {
        [Fact]
        public void Repository_Create_NewsItem_EqualFirstNames()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetNewsRepositoryMock();
            var repository = mockRepo.Object.NewsRepository;
            var newsItemToAdd = new DAL.Entities.News.News()
            {
                Id = 4,
                Title = "Fourth title",
                Text = "Fourth text",
                CreationDate = new DateTime(2020, 4, 3, 3, 3, 3, DateTimeKind.Utc),
                URL = "Url4",
                ImageId = 3
            };

            // Act
            var createdNewsItem = repository.Create(newsItemToAdd);

            // Assert
            createdNewsItem.Should().BeEquivalentTo(newsItemToAdd);
        }

        [Fact]
        public async Task Repository_GetAllNews_ReturnsAllNewsItems()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetNewsRepositoryMock();
            var repository = mockRepo.Object.NewsRepository;

            // Act
            var result = await repository.GetAllAsync(null, null);

            // Assert
            result.Should().HaveCount(3);
        }

        [Fact]
        public void Repository_NewsItemUpdate()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetNewsRepositoryMock();
            var repository = mockRepo.Object.NewsRepository;
            var newsItemToUpdate = new DAL.Entities.News.News()
            {
                Id = 1,
                Title = "First updated title",
                Text = "First updated text",
                CreationDate = new DateTime(2020, 1, 1, 1, 1, 1, DateTimeKind.Utc),
                URL = "Url1",
                ImageId = 1
            };

            // Act
            var updatedNewsItem = repository.Update(newsItemToUpdate);

            // Assert
            mockRepo.Verify(x => x.NewsRepository.Update(It.IsAny<DAL.Entities.News.News>()), Times.Once);
        }

        [Fact]
        public async Task Repository_DeleteNews_RemovesFromDatabase()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetNewsRepositoryMock();
            var repository = mockRepo.Object.NewsRepository;
            var newsItemIdToDelete = 1;

            // Act
            repository.Delete(new DAL.Entities.News.News { Id = newsItemIdToDelete });

            // Assert
            var deletedNewsItem = await repository.GetFirstOrDefaultAsync(u => u.Id == newsItemIdToDelete);
            deletedNewsItem.Should().BeNull();
        }
    }
}
