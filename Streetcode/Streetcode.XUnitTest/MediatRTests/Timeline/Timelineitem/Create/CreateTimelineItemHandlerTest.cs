namespace Streetcode.XUnitTest.MediatRTests.Timeline.Timelineitem.Create
{
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.Timeline;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Timeline;
    using Streetcode.BLL.MediatR.Timeline.TimelineItem.Create;
    using Streetcode.DAL.Entities.Timeline;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Can not test.
    /// </summary>
    public class CreateTimelineItemHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTimelineItemHandlerTest"/> class.
        /// </summary>
        public CreateTimelineItemHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetTimelineRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<TimelineItemProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        /// <summary>
        /// Created result should not be null.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task CreatedTimelineItemShouldNotBeNull()
        {
            // Arrange
            var historicalTimelines = new List<HistoricalContextDto>()
            {
                new HistoricalContextDto()
                {
                    Id = 1,
                },
                new HistoricalContextDto()
                {
                    Id = 2,
                },
                new HistoricalContextDto()
                {
                    Id = 3,
                },
            };

            var handler = new CreateTimelineItemHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(
                new CreateTimelineItemCommand(
                new BLL.Dto.Timeline.TimelineItemDto()
                {
                    Title = "TEST",
                    HistoricalContexts = historicalTimelines,
                }), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }

        /// <summary>
        /// Created result title should be "TEST".
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task CreatedTimelineItemTitleShouldBeTest()
        {
            // Arrange
            var historicalTimelines = new List<HistoricalContextDto>()
            {
                new HistoricalContextDto()
                {
                    Id = 1,
                },
                new HistoricalContextDto()
                {
                    Id = 2,
                },
                new HistoricalContextDto()
                {
                    Id = 3,
                },
            };

            var handler = new CreateTimelineItemHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(
                new CreateTimelineItemCommand(
                new BLL.Dto.Timeline.TimelineItemDto()
                {
                    Title = "TEST",
                    HistoricalContexts = historicalTimelines,
                }), CancellationToken.None);

            // Assert
            result.Value.Title.Should().Equals("TEST");
        }
    }
}
