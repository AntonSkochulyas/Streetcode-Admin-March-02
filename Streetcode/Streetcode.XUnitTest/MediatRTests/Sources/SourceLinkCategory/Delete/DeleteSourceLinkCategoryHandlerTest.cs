﻿using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Sources;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Delete;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Sources.SourceLinkCategory.Delete
{
    public class DeleteSourceLinkCategoryHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly IMapper _mapper;

        public DeleteSourceLinkCategoryHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetSourceRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<SourceLinkCategoryProfile>();
                c.AddProfile<SourceLinkSubCategoryProfile>();
                c.AddProfile<StreetcodeCategoryContentProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        [Fact]
        public async Task Handler_DeleteSourceLinkWithWrongId_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new DeleteSourceLinkCategoryHandler(_mockRepository.Object, _mockLogger.Object, _mapper);

            int wrongId = 10;
            var request = new DeleteSourceLinkCategoryCommand(wrongId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_DeleteSourceLinkCategoryWithCorrectId_DeleteShouldBeCalled()
        {
            // Arrange
            var handler = new DeleteSourceLinkCategoryHandler(_mockRepository.Object, _mockLogger.Object, _mapper);

            int correctId = 1;
            var request = new DeleteSourceLinkCategoryCommand(correctId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            _mockRepository.Verify(
                x => x.SourceCategoryRepository
                .Delete(It.IsAny<DAL.Entities.Sources.SourceLinkCategory>()), Times.Once);
        }

        [Fact]
        public async Task Handler_DeleteSourceLinkWithCorrectId_IsSuccessShouldBeTrue()
        {
            // Arrange
            var handler = new DeleteSourceLinkCategoryHandler(_mockRepository.Object, _mockLogger.Object, _mapper);

            int correctId = 1;
            var request = new DeleteSourceLinkCategoryCommand(correctId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}