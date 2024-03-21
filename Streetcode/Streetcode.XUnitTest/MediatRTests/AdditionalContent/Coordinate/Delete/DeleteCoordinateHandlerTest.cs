﻿namespace Streetcode.XUnitTest.MediatRTests.AdditionalContent.Coordinate.Delete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.Configuration.Conventions;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.AdditionalContent.Coordinates;
    using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create;
    using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Delete;
    using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.MediatRTests.Mocks;
    using Xunit;

    public class DeleteCoordinateHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public DeleteCoordinateHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetCoordinateRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<StreetcodeCoordinateProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        [Fact]
        public async Task Handler_WrongId_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new DeleteCoordinateHandler(_mockRepository.Object);

            int wrongId = 10;
            DeleteCoordinateCommand request = new DeleteCoordinateCommand(wrongId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_CorrectId_IsSuccessShouldBeTrue()
        {
            // Arrange
            var handler = new DeleteCoordinateHandler(_mockRepository.Object);

            int correctId = 1;
            DeleteCoordinateCommand request = new DeleteCoordinateCommand(correctId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
