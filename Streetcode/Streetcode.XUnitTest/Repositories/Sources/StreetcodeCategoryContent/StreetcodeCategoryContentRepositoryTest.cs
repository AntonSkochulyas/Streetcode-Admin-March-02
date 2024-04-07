using FluentAssertions;
using Moq;
using Streetcode.XUnitTest.Repositories.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.Repositories.Sources.StreetcodeCategoryContent
{
    public class StreetcodeCategoryContentRepositoryTest
    {
        [Fact]
        public void Repository_Create_StreetcodeCategoryContent_EqualFirstNames()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetStreetcodeCategoryContentRepositoryMock();
            var repository = mockRepo.Object.StreetcodeCategoryContentRepository;
            var streetcodeCategoryContentToAdd = new DAL.Entities.Sources.StreetcodeCategoryContent()
            {
                SourceLinkCategoryId = 4,
                Text = "Fourth text",
                StreetcodeId = 1
            };

            // Act
            var createdStreetcodeCategoryContent = repository.Create(streetcodeCategoryContentToAdd);

            // Assert
            createdStreetcodeCategoryContent.Should().BeEquivalentTo(streetcodeCategoryContentToAdd);
        }

        [Fact]
        public async Task Repository_GetAllNews_ReturnsAllStreetcodeCategoryContents()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetStreetcodeCategoryContentRepositoryMock();
            var repository = mockRepo.Object.StreetcodeCategoryContentRepository;

            // Act
            var result = await repository.GetAllAsync(null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public void Repository_StreetcodeCategoryContentUpdate()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetStreetcodeCategoryContentRepositoryMock();
            var repository = mockRepo.Object.StreetcodeCategoryContentRepository;
            var streetcodeCategoryContentToUpdate = new DAL.Entities.Sources.StreetcodeCategoryContent()
            {
                SourceLinkCategoryId = 1,
                Text = "First updated text",
                StreetcodeId = 1
            };

            // Act
            var updatedStreetcodeCategoryContent = repository.Update(streetcodeCategoryContentToUpdate);

            // Assert
            mockRepo.Verify(x => x.StreetcodeCategoryContentRepository.Update(It.IsAny<DAL.Entities.Sources.StreetcodeCategoryContent>()), Times.Once);
        }

        [Fact]
        public async Task Repository_DeleteStreetcodeCategoryContent_RemovesFromDatabase()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetStreetcodeCategoryContentRepositoryMock();
            var repository = mockRepo.Object.StreetcodeCategoryContentRepository;
            var streetcodeCategoryContentIdToDelete = 1;

            // Act
            repository.Delete(new DAL.Entities.Sources.StreetcodeCategoryContent { SourceLinkCategoryId = streetcodeCategoryContentIdToDelete });

            // Assert
            var deletedStreetcodeCategoryContent = await repository.GetFirstOrDefaultAsync(s => s.SourceLinkCategoryId == streetcodeCategoryContentIdToDelete);
            deletedStreetcodeCategoryContent.Should().BeNull();
        }
    }
}
