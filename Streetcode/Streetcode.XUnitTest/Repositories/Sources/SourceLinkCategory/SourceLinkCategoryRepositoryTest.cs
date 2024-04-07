using FluentAssertions;
using Moq;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.Repositories.Sources.SourceLinkCategory
{
    public class SourceLinkCategoryRepositoryTest
    {
        [Fact]
        public void Repository_Create_SourceLinkCategory_EqualFirstNames()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetSourceRepositoryMock();
            var repository = mockRepo.Object.SourceCategoryRepository;
            var sourceLinkCategoryToAdd = new DAL.Entities.Sources.SourceLinkCategory()
            {
                Id = 4,
                Title = "Fourth title",
                ImageId = 3
            };

            // Act
            var createdSourceLinkCategory = repository.Create(sourceLinkCategoryToAdd);

            // Assert
            createdSourceLinkCategory.Should().BeEquivalentTo(sourceLinkCategoryToAdd);
        }

        [Fact]
        public async Task Repository_GetAllSourceLinkCategories_ReturnsAllSourceLinkCategories()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetSourceRepositoryMock();
            var repository = mockRepo.Object.SourceCategoryRepository;

            // Act
            var result = await repository.GetAllAsync(null, null);

            // Assert
            result.Should().HaveCount(3);
        }

        [Fact]
        public void Repository_SourceLinkCategoryUpdate()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetSourceRepositoryMock();
            var repository = mockRepo.Object.SourceCategoryRepository;
            var sourceLinkCategoryToUpdate = new DAL.Entities.Sources.SourceLinkCategory()
            {
                Id = 1,
                Title = "First updated title",
                ImageId = 1
            };

            // Act
            var updatedSourceLinkCategory = repository.Update(sourceLinkCategoryToUpdate);

            // Assert
            mockRepo.Verify(x => x.SourceCategoryRepository.Update(It.IsAny<DAL.Entities.Sources.SourceLinkCategory>()), Times.Once);
        }

        [Fact]
        public async Task Repository_DeleteStreetcodeCategoryContent_RemovesFromDatabase()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetSourceRepositoryMock();
            var repository = mockRepo.Object.SourceCategoryRepository;
            var sourceLinkCategoryIdToDelete = 1;

            // Act
            repository.Delete(new DAL.Entities.Sources.SourceLinkCategory { Id = sourceLinkCategoryIdToDelete });

            // Assert
            var deletedSourceLinkCategory = await repository.GetFirstOrDefaultAsync(u => u.Id == sourceLinkCategoryIdToDelete);
            deletedSourceLinkCategory.Should().BeNull();
        }
    }
}
