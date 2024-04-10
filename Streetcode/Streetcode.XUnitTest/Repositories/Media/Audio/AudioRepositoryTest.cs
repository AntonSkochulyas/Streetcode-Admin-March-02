using System;
using FluentAssertions;
using Moq;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.Repositories.Media.Audio
{
	public class AudioRepositoryTest
	{
        [Fact]
        public void Repository_Create_Audio_EqualsDescription()
        {
            //Arrange
            var mockRepo = RepositoryMocker.GetAudiosRepositoryMock();
            var repository = mockRepo.Object.AudioRepository;
            var audioItemToAdd = new DAL.Entities.Media.Audio()
            {
                Id = 1,
                Base64 = "1Base",
                BlobName = "1Blob",
                MimeType = "1Mime",
                Streetcode = new DAL.Entities.Streetcode.StreetcodeContent(),
                Title = "1Title"
            };

            // Act
            var createdAudioItem = repository.Create(audioItemToAdd);

            // Assert
            createdAudioItem.Should().BeEquivalentTo(audioItemToAdd);
        }

        [Fact]
        public async Task Repository_GetAllAudios_ReturnsAllAudiosItems()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetAudiosRepositoryMock();
            var repository = mockRepo.Object.AudioRepository;

            // Act
            var result = await repository.GetAllAsync(null, null);

            // Assert
            result.Should().HaveCount(4);
        }

        [Fact]
        public void Repository_AudiosItemUpdate()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetAudiosRepositoryMock();
            var repository = mockRepo.Object.AudioRepository;
            var audioItemToUpdate = new DAL.Entities.Media.Audio()
            {
                Id = 1,
                Base64 = "1UpBase",
                BlobName = "1UpBlob",
                MimeType = "1UpMime",
                Streetcode = new DAL.Entities.Streetcode.StreetcodeContent(),
                Title = "1UpTitle"
            };

            // Act
            var updatedAudioItem = repository.Update(audioItemToUpdate);

            // Assert
            mockRepo.Verify(x => x.AudioRepository.Update(It.IsAny<DAL.Entities.Media.Audio>()), Times.Once);
        }

        [Fact]
        public async Task Repository_DeleteArt_RemovesFromDatabase()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetAudiosRepositoryMock();
            var repository = mockRepo.Object.AudioRepository;
            var audioItemIdToDelete = 1;

            // Act
            repository.Delete(new DAL.Entities.Media.Audio { Id = audioItemIdToDelete });

            // Assert
            var deletedAudioItem = await repository.GetFirstOrDefaultAsync(n => n.Id == audioItemIdToDelete);
            deletedAudioItem.Should().BeNull();
        }
    }
}