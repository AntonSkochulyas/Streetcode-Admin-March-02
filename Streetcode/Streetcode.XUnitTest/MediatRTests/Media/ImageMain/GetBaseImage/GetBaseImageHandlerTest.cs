using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Media.Images;
using Streetcode.BLL.MediatR.Media.Image.GetBaseImage;
using Streetcode.BLL.MediatR.Media.ImageMain.GetById;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Media.ImageMain.GetBaseImage
{
    public class GetBaseImageHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _blobService;

        public GetBaseImageHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetImageMainRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ImageMainProfile>();

            });

            _mockLogger = new Mock<ILoggerService>();

            _blobService = new Mock<IBlobService>();
        }

        [Fact]
        public async Task GetBaseImage_WrongId_IsFailedShouldBeTrue()
        {
            // Arrange
            int wrongId = 10;
            var handler = new GetBaseImageHandler(_blobService.Object, _mockRepository.Object, _mockLogger.Object);
            var request = new GetBaseImageQuery(wrongId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }
    }
}
