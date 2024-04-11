using System;
using FluentAssertions;
using Moq;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.Repositories.Transactions
{
	public class TransactionLinkRepositoryTest
	{
        [Fact]
        public void Repository_Create_TransactionLinkItem_EqualFirstNames()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetTransactionsRepositoryMock();
            var repository = mockRepo.Object.TransactLinksRepository;
            var transactionLinkItemToAdd = new DAL.Entities.Transactions.TransactionLink()
            {
                Id = 5,
                Url = "1Url",
                UrlTitle = "1UrlTitle",
                StreetcodeId = 5,
                Streetcode = new DAL.Entities.Streetcode.StreetcodeContent()
            };

            // Act
            var createdTransactionLinkItem = repository.Create(transactionLinkItemToAdd);

            // Assert
            createdTransactionLinkItem.Should().BeEquivalentTo(transactionLinkItemToAdd);
        }

        [Fact]
        public async Task Repository_GetAllTransactionLinks_ReturnsAllTransactionLinksItems()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetTransactionsRepositoryMock();
            var repository = mockRepo.Object.TransactLinksRepository;

            // Act
            var result = await repository.GetAllAsync(null, null);

            // Assert
            result.Should().HaveCount(4);
        }

        [Fact]
        public void Repository_TransactionLinksItemUpdate()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetTransactionsRepositoryMock();
            var repository = mockRepo.Object.TransactLinksRepository;
            var transactionLinkItemToUpdate = new DAL.Entities.Transactions.TransactionLink()
            {
                Id = 5,
                Url = "1UpUrl",
                UrlTitle = "1UpUrlTitle",
                StreetcodeId = 5,
                Streetcode = new DAL.Entities.Streetcode.StreetcodeContent()
            };

            // Act
            var updatedTransactionLinkItem = repository.Update(transactionLinkItemToUpdate);

            // Assert
            mockRepo.Verify(x => x.TransactLinksRepository.Update(It.IsAny<DAL.Entities.Transactions.TransactionLink>()), Times.Once);
        }

        [Fact]
        public async Task Repository_DeleteTransactionLink_RemovesFromDatabase()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetTransactionsRepositoryMock();
            var repository = mockRepo.Object.TransactLinksRepository;
            var transactionLinkItemIdToDelete = 1;

            // Act
            repository.Delete(new DAL.Entities.Transactions.TransactionLink { Id = transactionLinkItemIdToDelete });

            // Assert
            var deletedTransactionLinkItem = await repository.GetFirstOrDefaultAsync(n => n.Id == transactionLinkItemIdToDelete);
            deletedTransactionLinkItem.Should().BeNull();
        }
    }
}