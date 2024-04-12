namespace Streetcode.XUnitTest.MediatRTests.News.GetById
{
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.News;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Newss;
    using Streetcode.BLL.MediatR.Newss.GetById;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    public class GetNewsByIdHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _blobService;

        public GetNewsByIdHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetNewsRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<NewsProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();

            _blobService = new Mock<IBlobService>();
        }

        [Fact]
        public async Task Handler_GetNewsByValidId_ResultShouldBeNotNull()
        {
            // Arrange
            var handler = new GetNewsByIdHandler(_mapper, _mockRepository.Object, _blobService.Object, _mockLogger.Object);
            int validId = 1;
            var request = new GetNewsByIdQuery(validId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task Handler_GetNewsByInvalidId_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new GetNewsByIdHandler(_mapper, _mockRepository.Object, _blobService.Object, _mockLogger.Object);
            int invalidId = 10;
            var request = new GetNewsByIdQuery(invalidId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_GetNewsByValidId_ResultShouldBeTypeOfNewsDto()
        {
            // Arrange
            var handler = new GetNewsByIdHandler(_mapper, _mockRepository.Object, _blobService.Object, _mockLogger.Object);
            int validId = 1;
            var request = new GetNewsByIdQuery(validId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().BeOfType<NewsDto>();
        }
    }
}
