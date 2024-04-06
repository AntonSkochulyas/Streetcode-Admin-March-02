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
using Streetcode.BLL.MediatR.Timeline.HistoricalContext.GetAll;

namespace Streetcode.XUnitTest.MediatRTests.Timeline.HistoricalContext.GetAll
{
    public class GetAllHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public GetAllHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetHistoricalContextRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<HistoricalContextProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        [Fact]
        public async Task Get_All_Not_Null_Or_Empty_Test()
        {
            //Arrange
            var handler = new GetAllHistoricalContextHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            //Act
            var result = await handler.Handle(new GetAllHistoricalContextQuery(), CancellationToken.None);

            //Assert        
            result.Value.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_All_Count_Should_Be_Two()
        {
            //Arrange
            var handler = new GetAllHistoricalContextHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            //Act
            var result = await handler.Handle(new GetAllHistoricalContextQuery(), CancellationToken.None);

            //Assert        
            result.Value.Count().Should().Be(3);
        }

        [Fact]
        public async Task Get_All_Should_Be_Type_List_TimelineItemDTO()
        {
            //Arrange
            var handler = new GetAllHistoricalContextHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            //Act
            var result = await handler.Handle(new GetAllHistoricalContextQuery(), CancellationToken.None);

            //Assert        
            result.Value.Should().BeOfType<List<HistoricalContextDto>>();
        }
    }
}