namespace Streetcode.XUnitTest.Mocks;

using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.DAL.Entities.Timeline;
using Streetcode.DAL.Enums;
using Streetcode.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;

internal partial class RepositoryMocker
{
    public static Mock<IRepositoryWrapper> GetTimelineRepositoryMock()
    {
        var historicalTimelines = new List<HistoricalContextTimeline>()
            {
                new HistoricalContextTimeline()
                {
                    HistoricalContextId = 1,
                    TimelineId = 1,
                },
                new HistoricalContextTimeline()
                {
                    HistoricalContextId = 2,
                    TimelineId = 1,
                },
                new HistoricalContextTimeline()
                {
                    HistoricalContextId = 3,
                    TimelineId = 2,
                },
            };

        var timelineItems = new List<TimelineItem>()
        {
            new TimelineItem
            {
                Id = 1,
                Title = "TimelineItem 1",
                Description = "First description",
                Date = DateTime.Now,
                DateViewPattern = DateViewPattern.DateMonthYear,
                HistoricalContextTimelines = historicalTimelines
            },
            new TimelineItem
            {
                Id = 2,
                Title = "TimelineItem 2",
                Description = "Second description",
                Date = DateTime.Now.AddDays(1),
                DateViewPattern = DateViewPattern.DateMonthYear,
                HistoricalContextTimelines = historicalTimelines
            },
            new TimelineItem
            {
                Id = 3,
                Title = "TimelineItem 3",
                Description = "Third description",
                Date = DateTime.Now,
                DateViewPattern = DateViewPattern.DateMonthYear,
                HistoricalContextTimelines = historicalTimelines
            }
        };

        var mockRepo = new Mock<IRepositoryWrapper>();

        mockRepo.Setup(repo => repo.TimelineRepository.GetAllAsync(It.IsAny<Expression<Func<TimelineItem, bool>>>(), It.IsAny<Func<IQueryable<TimelineItem>,
            IIncludableQueryable<TimelineItem, object>>>())).ReturnsAsync(timelineItems);

        mockRepo.Setup(x => x.TimelineRepository.GetFirstOrDefaultAsync(
             It.IsAny<Expression<Func<TimelineItem, bool>>>(),
             It.IsAny<Func<IQueryable<TimelineItem>, IIncludableQueryable<TimelineItem, object>>>()))
             .ReturnsAsync((Expression<Func<TimelineItem, bool>> predicate, Func<IQueryable<TimelineItem>,
             IIncludableQueryable<TimelineItem, object>> include) =>
             {
                 return timelineItems.FirstOrDefault(predicate.Compile());
             });

        mockRepo.Setup(x => x.HistoricalContextTimelineRepository.CreateAsync(It.IsAny<HistoricalContextTimeline>())).ReturnsAsync(historicalTimelines[0]);

        mockRepo.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        mockRepo.Setup(x => x.TimelineRepository.Create(It.IsAny<TimelineItem>()))
             .Returns((TimelineItem timelineItem) =>
             {
                 timelineItems.Add(timelineItem);
                 return timelineItem;
             });

        mockRepo.Setup(x => x.TimelineRepository.Delete(It.IsAny<TimelineItem>()))
        .Callback((TimelineItem timelineItem) =>
        {
            timelineItem = timelineItems.FirstOrDefault(x => x.Id == timelineItem.Id);
            if(timelineItem != null)
            {
                timelineItems.Remove(timelineItem);
            }
        });

        return mockRepo;
    }
}
