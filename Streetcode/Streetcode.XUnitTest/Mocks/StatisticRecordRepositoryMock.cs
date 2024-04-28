using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Analytics;
using Streetcode.DAL.Entities.News;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.XUnitTest.Mocks
{
    internal partial class RepositoryMocker
    {
        public static Mock<IRepositoryWrapper> GetStatisticRecordRepositoryMock()
        {
            var statisticRecords = new List<StatisticRecord>()
            {
                new StatisticRecord
                {
                    Id = 1,
                    Address = "Adderss1",
                    QrId = 1,
                    StreetcodeCoordinateId = 1
                },
                new StatisticRecord
                {
                    Id = 2,
                    Address = "Adderss2",
                    QrId = 2,
                    StreetcodeCoordinateId = 2
                },
                new StatisticRecord
                {
                    Id = 3,
                    Address = "Adderss3",
                    QrId = 3,
                    StreetcodeCoordinateId = 2
                }
            };

            var mockRepo = new Mock<IRepositoryWrapper>();

            mockRepo.Setup(x => x.StatisticRecordRepository.Create(It.IsAny<StatisticRecord>()))
            .Returns((StatisticRecord statisticRecord) =>
            {
                statisticRecords.Add(statisticRecord);
                return statisticRecord;
            });

            mockRepo.Setup(x => x.StatisticRecordRepository.GetFirstOrDefaultAsync(
            It.IsAny<Expression<Func<StatisticRecord, bool>>>(),
            It.IsAny<Func<IQueryable<StatisticRecord>, IIncludableQueryable<StatisticRecord, object>>>()))
            .ReturnsAsync((Expression<Func<StatisticRecord, bool>> predicate, Func<IQueryable<StatisticRecord>,
            IIncludableQueryable<News, object>> include) =>
            {
                return statisticRecords.FirstOrDefault(predicate.Compile());
            });

            mockRepo.Setup(x => x.StatisticRecordRepository.GetAllAsync(
                It.IsAny<Expression<Func<StatisticRecord, bool>>>(),
                It.IsAny<Func<IQueryable<StatisticRecord>, IIncludableQueryable<StatisticRecord, object>>>()))
                .ReturnsAsync(statisticRecords);

            mockRepo.Setup(x => x.StatisticRecordRepository.Delete(It.IsAny<StatisticRecord>()))
                .Callback((StatisticRecord statisticRecord) =>
                {
                    statisticRecord = statisticRecords.FirstOrDefault(x => x.Id == statisticRecord.Id);
                    if (statisticRecord is not null)
                    {
                        statisticRecords.Remove(statisticRecord);
                    }
                });

            mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            return mockRepo;
        }
    }
}
