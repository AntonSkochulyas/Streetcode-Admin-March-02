using AutoMapper;
using Moq;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Media.Images;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Create;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;
using Streetcode.BLL.MediatR.Media.ImageMain.Create;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Dto.Media.Images;
using FluentAssertions;

namespace Streetcode.XUnitTest.MediatRTests.Media.ImageMain.Create
{
    public class CreateImageMainHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _blobService;

        public CreateImageMainHandlerTest()
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
        public async Task Handle_ImageMainDtoIsNull_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new CreateImageMainHandler(_blobService.Object, _mockRepository.Object, _mapper, _mockLogger.Object);
            ImageFileBaseCreateDto? imageMainDto = null;
            var request = new CreateImageMainCommand(imageMainDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ImageMainDtoValid_IsSuccessShouldBeTrue()
        {
            // Arrange
            var handler = new CreateImageMainHandler(_blobService.Object, _mockRepository.Object, _mapper, _mockLogger.Object);
            var imageMainDto = new ImageFileBaseCreateDto()
            {
                Alt = "Портрет Тараса Шевченка",
                Title = "Портрет Тараса Шевченка",
                MimeType = "image/gif"
            };
            var request = new CreateImageMainCommand(imageMainDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ImageMainDtoValid_ResultIsTypeOfImageMainDto()
        {
            // Arrange
            var handler = new CreateImageMainHandler(_blobService.Object, _mockRepository.Object, _mapper, _mockLogger.Object);
            var imageMainDto = new ImageFileBaseCreateDto()
            {
                Alt = "Портрет Тараса Шевченка",
                Title = "Портрет Тараса Шевченка",
                MimeType = "image/gif"
            };
            var request = new CreateImageMainCommand(imageMainDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().BeOfType<ImageMainDto>();
        }
    }
}
