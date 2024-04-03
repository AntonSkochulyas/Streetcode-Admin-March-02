using FluentAssertions;
using Moq;
using Streetcode.DAL.Entities.Partners;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.XUnitTest.Repositories.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.Repositories.Partners
{
    public class PartnerRepositoryTest
    {
        [Fact]
        public async Task Repository_Create_Partner_EqualTitles()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetPartnersRepositoryMock();
            var repository = mockRepo.Object.PartnersRepository;
            var partnerToAdd = new Partner { Id = 5, Title = "Fifth partner" };

            // Act
            var createdPartner = repository.Create(partnerToAdd);

            // Assert
            createdPartner.Title.Should().Be(partnerToAdd.Title);
        }

        [Fact]
        public async Task Repository_GetAllPartners_ReturnsAllPartners()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetPartnersRepositoryMock();
            var repository = mockRepo.Object.PartnersRepository;

            // Act
            var result = await repository.GetAllAsync(null, null);

            // Assert
            result.Count().Should().Be(4);
        }

        [Fact]
        public async Task Repository_SPartnerUpdate()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetPartnersRepositoryMock();
            var repository = mockRepo.Object.PartnersRepository;
            var partnerToUpdate = new Partner { Id = 1, Title = "Updated first title" };

            // Act
            var updatedPartner = repository.Update(partnerToUpdate);

            // Assert
            mockRepo.Verify(x => x.PartnersRepository.Update(It.IsAny<Partner>()), Times.Once);
        }

        [Fact]
        public async Task Repository_DeletePartner_RemovesFromDatabase()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetPartnersRepositoryMock();
            var repository = mockRepo.Object.PartnersRepository;
            var partnerIdToDelete = 1;

            // Act
            repository.Delete(new Partner { Id = partnerIdToDelete });

            // Assert
            var deletedPartner= await repository.GetFirstOrDefaultAsync(u => u.Id == partnerIdToDelete);
            deletedPartner.Should().BeNull();
        }
    }
}
