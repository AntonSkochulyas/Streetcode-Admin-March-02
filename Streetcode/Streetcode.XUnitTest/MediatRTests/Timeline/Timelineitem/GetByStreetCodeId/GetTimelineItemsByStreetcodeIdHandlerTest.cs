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
using Streetcode.BLL.MediatR.Media.Audio.GetByStreetcodeId;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.GetByStreetcodeId;
using Streetcode.BLL.Dto.Timeline;

namespace Streetcode.XUnitTest.MediatRTests.Timeline.Timelineitem.GetByStreetCodeId
{
    public class GetTimelineItemsByStreet
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;


        public GetTimelineItemsByStreet()
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
            var handler = new GetTimelineItemsByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetTimelineItemsByStreetcodeIdQuery(1), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }
    }
}