using FluentAssertions;
using Moq;
using Streetcode.DAL.Entities.Timeline;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.Repositories.Timeline
{
    public class TimelineRepositoryTest
    {
        [Fact]
        public async Task Repository_Create_TimelineItem_EqualFirstNames()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetTimelineRepositoryMock();
            var repository = mockRepo.Object.TimelineRepository;
            var timelineItemToAdd = new TimelineItem { Id = 5, Date = DateTime.Now, Title = "First Event", Description = "Description of the first event" };

            // Act
            var createdTimelineItem = repository.Create(timelineItemToAdd);

            // Assert
            createdTimelineItem.Should().BeEquivalentTo(timelineItemToAdd);
        }

        [Fact]
        public async Task Repository_GetAllTimeline_ReturnsAllTimelineItems()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetTimelineRepositoryMock();
            var repository = mockRepo.Object.TimelineRepository;

            // Act
            var result = await repository.GetAllAsync(null, null);

            // Assert
            result.Should().HaveCount(3);
        }

        [Fact]
        public async Task Repository_TimelineItemUpdate()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetTimelineRepositoryMock();
            var repository = mockRepo.Object.TimelineRepository;
            var timelineItemToUpdate = new TimelineItem { Id = 1, Date = DateTime.Now, Title = "First Event", Description = "Updated description" };

            // Act
            var updatedTimelineItem = repository.Update(timelineItemToUpdate);

            // Assert
            mockRepo.Verify(x => x.TimelineRepository.Update(It.IsAny<TimelineItem>()), Times.Once);
        }

        [Fact]
        public async Task Repository_DeleteTimeline_RemovesFromDatabase()
        {
            // Arrange
            var mockRepo = RepositoryMocker.GetTimelineRepositoryMock();
            var repository = mockRepo.Object.TimelineRepository;
            var timelineItemIdToDelete = 1;

            // Act
            repository.Delete(new TimelineItem { Id = timelineItemIdToDelete });

            // Assert
            var deletedTimelineItem = await repository.GetFirstOrDefaultAsync(u => u.Id == timelineItemIdToDelete);
            deletedTimelineItem.Should().BeNull();
        }
    }
}
