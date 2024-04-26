using System;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;
using Streetcode.DAL.Entities.Transactions;
using Streetcode.DAL.Entities.Sources;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;

namespace Streetcode.XUnitTest.Mocks
{
	internal partial class RepositoryMocker
	{
        public static Mock<IRepositoryWrapper> GetTransactionsRepositoryMock()
        {
            var transactions = new List<TransactionLink>
            {
                new TransactionLink() { Id = 1, Url = "1Url", UrlTitle = "1UrlTitle", StreetcodeId = 1, Streetcode = new DAL.Entities.Streetcode.StreetcodeContent() },
                new TransactionLink() { Id = 2, Url = "2Url", UrlTitle = "2UrlTitle", StreetcodeId = 2, Streetcode = new DAL.Entities.Streetcode.StreetcodeContent() },
                new TransactionLink() { Id = 3, Url = "3Url", UrlTitle = "3UrlTitle", StreetcodeId = 3, Streetcode = new DAL.Entities.Streetcode.StreetcodeContent() },
                new TransactionLink() { Id = 4, Url = "4Url", UrlTitle = "4UrlTitle", StreetcodeId = 4, Streetcode = new DAL.Entities.Streetcode.StreetcodeContent() },
            };

            var mockRepo = new Mock<IRepositoryWrapper>();

            mockRepo.Setup(x => x.TransactLinksRepository
                .GetAllAsync(
                    It.IsAny<Expression<Func<TransactionLink, bool>>>(),
                    It.IsAny<Func<IQueryable<TransactionLink>, IIncludableQueryable<TransactionLink, object>>>()))
                .ReturnsAsync(transactions);

            mockRepo.Setup(x => x.TransactLinksRepository.Create(It.IsAny<TransactionLink>()))
                .Returns((TransactionLink transactionLink) =>
                {
                    transactions.Add(transactionLink);
                    return transactionLink;
                });

            mockRepo.Setup(x => x.TransactLinksRepository.Delete(It.IsAny<TransactionLink>()))
            .Callback((TransactionLink transactionLink) =>
            {
                transactionLink = transactions.FirstOrDefault(x => x.Id == transactionLink.Id);
                if (transactionLink is not null)
                {
                    transactions.Remove(transactionLink);
                }
            });

            return mockRepo;
        }
    }
}