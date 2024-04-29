using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.Media.Images;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Media.Images;
using Streetcode.BLL.MediatR.Media.ImageMain.GetById;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Media.ImageMain.GetById
{
    public class GetImageMainByIdHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _blobService;

        public GetImageMainByIdHandlerTest()
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
        public async Task GetImageMainById_WrongId_IsFailedShouldBeTrue()
        {
            // Arrange
            int wrongId = 10;
            var handler = new GetImageMainByIdHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);
            var request = new GetImageMainByIdQuery(wrongId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task GetImageMainById_CorrectId_IsSuccessShouldBeTrue()
        {
            // Arrange
            int correctId = 1;
            var handler = new GetImageMainByIdHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);
            var request = new GetImageMainByIdQuery(correctId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task GetImageMainById_CorrectId_ResultShouldBeOfTypeImageMainDto()
        {
            // Assert
            int correctId = 1;
            var handler = new GetImageMainByIdHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);
            var request = new GetImageMainByIdQuery(correctId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().BeOfType<ImageMainDto>();
        }
    }
}
