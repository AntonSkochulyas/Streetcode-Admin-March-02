using FluentAssertions;
using Moq;
using Streetcode.XUnitTest.Repositories.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.Repositories.AdditionalContent.Tag
{
    public class TagRepositoryTest
    {
        [Fact]
        public void Repository_Create_Tag_EqualFirstNames()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetTagRepositoryMock();
            var repository = mockRepo.Object.TagRepository;
            var tagToAdd = new DAL.Entities.AdditionalContent.Tag()
            {
                Id = 3,
                Title = "Title"
            };

            // Act
            var createdTag = repository.Create(tagToAdd);

            // Assert
            createdTag.Should().BeEquivalentTo(tagToAdd);
        }

        [Fact]
        public async Task Repository_GetAllTags_ReturnsAllTags()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetTagRepositoryMock();
            var repository = mockRepo.Object.TagRepository;

            // Act
            var result = await repository.GetAllAsync(null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [Fact]
        public void Repository_TagUpdate()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetTagRepositoryMock();
            var repository = mockRepo.Object.TagRepository;
            var tagToUpdate = new DAL.Entities.AdditionalContent.Tag()
            {
                Id = 1,
                Title = "Update title"
            };

            // Act
            var updatedTag = repository.Update(tagToUpdate);

            // Assert
            mockRepo.Verify(x => x.TagRepository.Update(It.IsAny<DAL.Entities.AdditionalContent.Tag>()), Times.Once);
        }

        [Fact]
        public async Task Repository_DeleteTag_RemovesFromDatabase()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetTagRepositoryMock();
            var repository = mockRepo.Object.TagRepository;
            var tagIdToDelete = 1;

            // Act
            repository.Delete(new DAL.Entities.AdditionalContent.Tag { Id = tagIdToDelete });

            // Assert
            var deletedTag = await repository.GetFirstOrDefaultAsync(s => s.Id == tagIdToDelete);
            deletedTag.Should().BeNull();
        }
    }
}
