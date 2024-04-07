using FluentAssertions;
using Moq;
using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;
using Streetcode.XUnitTest.Repositories.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.Repositories.AdditionalContent.Subtitle
{
    public class SubtitleRepositoryTest
    {
        [Fact]
        public void Repository_Create_Subtitle_EqualFirstNames()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetSubtitleRepositoryMock();
            var repository = mockRepo.Object.SubtitleRepository;
            var subtitleToAdd = new DAL.Entities.AdditionalContent.Subtitle()
            {
                Id = 4,
                StreetcodeId = 1
            };

            // Act
            var createdSubtitle = repository.Create(subtitleToAdd);

            // Assert
            createdSubtitle.Should().BeEquivalentTo(subtitleToAdd);
        }

        [Fact]
        public async Task Repository_GetAllSubtitles_ReturnsAllSubtitles()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetSubtitleRepositoryMock();
            var repository = mockRepo.Object.SubtitleRepository;

            // Act
            var result = await repository.GetAllAsync(null, null);

            // Assert
            result.Should().HaveCount(3);
        }

        [Fact]
        public void Repository_SubtitleUpdate()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetSubtitleRepositoryMock();
            var repository = mockRepo.Object.SubtitleRepository;
            var subtitleToUpdate = new DAL.Entities.AdditionalContent.Subtitle()
            {
                Id = 1,
                StreetcodeId = 1,
                SubtitleText = "Updated text"
            };

            // Act
            var updatedSubtitle = repository.Update(subtitleToUpdate);

            // Assert
            mockRepo.Verify(x => x.SubtitleRepository.Update(It.IsAny<DAL.Entities.AdditionalContent.Subtitle>()), Times.Once);
        }

        [Fact]
        public async Task Repository_DeleteSubtitle_RemovesFromDatabase()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetSubtitleRepositoryMock();
            var repository = mockRepo.Object.SubtitleRepository;
            var subtitleIdToDelete = 1;

            // Act
            repository.Delete(new DAL.Entities.AdditionalContent.Subtitle { Id = subtitleIdToDelete });

            // Assert
            var deletedSubtitle = await repository.GetFirstOrDefaultAsync(s => s.Id == subtitleIdToDelete);
            deletedSubtitle.Should().BeNull();
        }
    }
}
