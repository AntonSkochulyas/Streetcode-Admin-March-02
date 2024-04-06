using System;
using AutoMapper;
using Moq;
using Streetcode.BLL.Dto.Timeline;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Timeline;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.GetAll;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;
using FluentAssertions;

namespace Streetcode.XUnitTest.MediatRTests.Timeline.Timelineitem.GetAll
{
    public class GetAllHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public GetAllHandlerTest()
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
        public async Task Get_All_Not_Null_Or_Empty_Test()
        {
            //Arrange
            var handler = new GetAllTimelineItemsHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            var request = new GetAllTimelineItemsQuery();

            //Act
            var result = await handler.Handle(new GetAllTimelineItemsQuery(), CancellationToken.None);

            //Assert        
            result.Value.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_All_Count_Should_Be_Two()
        {
            //Arrange
            var handler = new GetAllTimelineItemsHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            //Act
            var result = await handler.Handle(new GetAllTimelineItemsQuery(), CancellationToken.None);

            //Assert        
            result.Value.Count().Should().Be(3);
        }

        [Fact]
        public async Task Get_All_Should_Be_Type_List_TimelineItemDTO()
        {
            //Arrange
            var handler = new GetAllTimelineItemsHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            //Act
            var result = await handler.Handle(new GetAllTimelineItemsQuery(), CancellationToken.None);

            //Assert        
            result.Value.Should().BeOfType<List<TimelineItemDto>>();
        }
    }
}