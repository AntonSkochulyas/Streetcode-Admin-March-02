namespace Streetcode.XUnitTest.MediatRTests.Timeline.Timelineitem.Update
{
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using global::Streetcode.BLL.Dto.Timeline;
    using global::Streetcode.BLL.Interfaces.Logging;
    using global::Streetcode.BLL.Mapping.Timeline;
    using global::Streetcode.BLL.MediatR.Timeline.TimelineItem.Update;
    using global::Streetcode.DAL.Repositories.Interfaces.Base;
    using global::Streetcode.XUnitTest.Mocks;
    using Moq;
    using Xunit;

    /// <summary>
    /// Can not test.
    /// </summary>
    public class UpdateTimelineItemHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTimelineItemHandlerTest"/> class.
        /// </summary>
        public UpdateTimelineItemHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetTimelineRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<TimelineItemProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        [Fact]
        public async Task Handler_TimelineDTOIsNull_IsFaildeShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateTimelineItemHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            TimelineItemDto? timeline = null;
            var request = new UpdateTimelineItemCommand(timeline);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_ValidData_IsSucessShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateTimelineItemHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            var timeline = new TimelineItemDto()
            {
               Id = 1,
               Title = "BEBEBBEBE",
               StreetcodeId = 1,
            };

            var updatedTimeline = new UpdateTimelineItemCommand(timeline);

            // Act
            var result = await handler.Handle(updatedTimeline, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
