﻿using System;
using AutoMapper;
using Moq;
using Streetcode.BLL.Dto.Timeline;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Timeline;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.GetAll;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.MediatRTests.Mocks;
using Xunit;
using FluentAssertions;
using Streetcode.BLL.MediatR.Media.Art.GetById;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.GetById;

namespace Streetcode.XUnitTest.MediatRTests.Timeline.Timelineitem.GetById
{
    public class GetByIdHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public GetByIdHandlerTest()
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
        public async Task GetByIdNotNullTest()
        {
            // Arrange
            var handler = new GetTimelineItemByIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetTimelineItemByIdQuery(1), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetByIdFirstShouldBeFirstTest()
        {
            // Arrange
            var handler = new GetTimelineItemByIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetTimelineItemByIdQuery(1), CancellationToken.None);

            // Assert
            result.Value.Title.Should().Be("TimelineItem 1");
        }
    }
}