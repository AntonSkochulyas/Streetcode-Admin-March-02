using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Media.Images;
using Streetcode.BLL.MediatR.Media.ImageMain.Delete;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Delete;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Media.ImageMain.Delete
{
    public class DeleteImageMainHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _blobService;

        public DeleteImageMainHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetImageMainRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ImageMainProfile>();

            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();

            _blobService = new Mock<IBlobService>();
        }

        [Fact]
        public async Task Handler_DeleteImageMainWithWrongId_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new DeleteImageMainHandler(_mockRepository.Object, _blobService.Object, _mockLogger.Object, _mapper);

            int wrongId = 10;
            var request = new DeleteImageMainCommand(wrongId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_DeleteImageMainWithCorrectId_DeleteShouldBeCalled()
        {
            // Arrange
            var handler = new DeleteImageMainHandler(_mockRepository.Object, _blobService.Object, _mockLogger.Object, _mapper);

            int correctId = 1;
            var request = new DeleteImageMainCommand(correctId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            _mockRepository.Verify(
                x => x.ImageMainRepository
                .Delete(It.IsAny<DAL.Entities.Media.Images.ImageMain>()), Times.Once);
        }

        [Fact]
        public async Task Handler_DeleteImageMainkWithCorrectId_IsSuccessShouldBeTrue()
        {
            // Arrange
            var handler = new DeleteImageMainHandler(_mockRepository.Object, _blobService.Object, _mockLogger.Object, _mapper);

            int correctId = 1;
            var request = new DeleteImageMainCommand(correctId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
