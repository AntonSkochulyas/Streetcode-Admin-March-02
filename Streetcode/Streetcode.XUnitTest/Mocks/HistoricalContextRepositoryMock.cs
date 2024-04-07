namespace Streetcode.XUnitTest.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Timeline;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    public static Mock<IRepositoryWrapper> GetHistoricalContextRepositoryMock()
    {
        var historical_contexts = new List<HistoricalContext>()
            {
                new HistoricalContext { Id = 1, Title = "TimelineItem 1" },
                new HistoricalContext { Id = 2, Title = "TimelineItem 2" },
                new HistoricalContext { Id = 3, Title = "TimelineItem 3" }
            };

        var mockRepo = new Mock<IRepositoryWrapper>();

        mockRepo.Setup(repo => repo.HistoricalContextRepository.GetAllAsync(It.IsAny<Expression<Func<HistoricalContext, bool>>>(), It.IsAny<Func<IQueryable<HistoricalContext>,
            IIncludableQueryable<HistoricalContext, object>>>())).ReturnsAsync(historical_contexts);

        mockRepo.Setup(repo => repo.HistoricalContextRepository.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<HistoricalContext, bool>>>(), It.IsAny<Func<IQueryable<HistoricalContext>, IIncludableQueryable<HistoricalContext, object>>>()))
                .ReturnsAsync((Expression<Func<HistoricalContext, bool>> predicate, Func<IQueryable<HistoricalContext>, IIncludableQueryable<HistoricalContext, object>> include) =>
                {
                    return historical_contexts.FirstOrDefault(predicate.Compile());
                });

        mockRepo.Setup(x => x.HistoricalContextRepository.Create(It.IsAny<HistoricalContext>())).Returns(historical_contexts[0]);
        mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        return mockRepo;
    }
}
