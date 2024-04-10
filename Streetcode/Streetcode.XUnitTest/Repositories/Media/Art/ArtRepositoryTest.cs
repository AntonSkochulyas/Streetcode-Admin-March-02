using System;
using FluentAssertions;
using Moq;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.Repositories.Media.Art
{
	public class ArtRepositoryTest
	{
		[Fact]
		public void Repository_Create_Art_EqualsDescription()
		{
            //Arrange
            var mockRepo = RepositoryMocker.GetArtRepositoryMock();
            var repository = mockRepo.Object.ArtRepository;
            var artItemToAdd = new DAL.Entities.Media.Images.Art()
            {
                Id = 1,
                Description = "1Description",
                Title = "1Title",
                Image = new DAL.Entities.Media.Images.Image(),
                ImageId = 1,
                StreetcodeArts = new List<DAL.Entities.Streetcode.StreetcodeArt>()
            };

            // Act
            var createdArtItem = repository.Create(artItemToAdd);

            // Assert
            createdArtItem.Should().BeEquivalentTo(artItemToAdd);
        }

		[Fact]
		public async Task Repository_GetAllArts_ReturnsAllArtsItems()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetArtRepositoryMock();
            var repository = mockRepo.Object.ArtRepository;

            // Act
            var result = await repository.GetAllAsync(null, null);

            // Assert
            result.Should().HaveCount(4);
        }

		[Fact]
		public void Repository_NewsItemUpdate()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetArtRepositoryMock();
            var repository = mockRepo.Object.ArtRepository;
            var artItemToUpdate = new DAL.Entities.Media.Images.Art()
            {
                Id = 1,
                Description = "1UpDescription",
                Title = "1UpTitle",
                Image = new DAL.Entities.Media.Images.Image(),
                ImageId = 1,
                StreetcodeArts = new List<DAL.Entities.Streetcode.StreetcodeArt>()
            };

            // Act
            var updatedArtItem = repository.Update(artItemToUpdate);

            // Assert
            mockRepo.Verify(x => x.ArtRepository.Update(It.IsAny<DAL.Entities.Media.Images.Art>()), Times.Once);
        }

		[Fact]
		public async Task Repository_DeleteArt_RemovesFromDatabase()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetArtRepositoryMock();
            var repository = mockRepo.Object.ArtRepository;
            var artItemIdToDelete = 1;

            // Act
            repository.Delete(new DAL.Entities.Media.Images.Art { Id = artItemIdToDelete });

            // Assert
            var deletedArtItem = await repository.GetFirstOrDefaultAsync(n => n.Id == artItemIdToDelete);
            deletedArtItem.Should().BeNull();
        }
    }
}