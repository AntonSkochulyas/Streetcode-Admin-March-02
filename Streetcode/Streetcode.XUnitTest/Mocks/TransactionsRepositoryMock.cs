using System;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;
using Streetcode.DAL.Entities.Transactions;

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

            return mockRepo;
        }
    }
}