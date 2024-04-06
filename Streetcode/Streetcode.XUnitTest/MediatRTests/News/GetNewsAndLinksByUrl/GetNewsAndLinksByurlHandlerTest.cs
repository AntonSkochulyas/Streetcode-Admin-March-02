namespace Streetcode.XUnitTest.MediatRTests.News.GetNewsAndLinksByUrl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.News;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Newss;
    using Streetcode.BLL.MediatR.Newss.GetByUrl;
    using Streetcode.BLL.MediatR.Newss.GetNewsAndLinksByUrl;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    public class GetNewsAndLinksByurlHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _blobService;

        public GetNewsAndLinksByurlHandlerTest()
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
        public async Task Handler_GetNewsAndLinksByInvalidUrl_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new GetNewsAndLinksByUrlHandler(_mapper, _mockRepository.Object, _blobService.Object, _mockLogger.Object);
            string invalidUrl = string.Empty;
            var request = new GetNewsAndLinksByUrlQuery(invalidUrl);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_GetNewsAndLinksByValidUrl_PreviousNewsLinkShouldBeAsExpected()
        {
            // Arrange
            var handler = new GetNewsAndLinksByUrlHandler(_mapper, _mockRepository.Object, _blobService.Object, _mockLogger.Object);
            string validUrl = "example2.com";
            string expectedPreviousNewsUrl = "example1.com";
            var request = new GetNewsAndLinksByUrlQuery(validUrl);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.PrevNewsUrl.Should().Be(expectedPreviousNewsUrl);
        }

        [Fact]
        public async Task Handler_GetNewsAndLinksByValidUrl_NextNewsLinkShouldBeAsExpected()
        {
            // Arrange
            var handler = new GetNewsAndLinksByUrlHandler(_mapper, _mockRepository.Object, _blobService.Object, _mockLogger.Object);
            string validUrl = "example2.com";
            string expectedNextNewsUrl = "example3.com";
            var request = new GetNewsAndLinksByUrlQuery(validUrl);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.NextNewsUrl.Should().Be(expectedNextNewsUrl);
        }

        [Fact]
        public async Task Handler_GetNewsAndLinksByValidUrl_PreviousNewsLinkShouldBeNull()
        {
            // Arrange
            var handler = new GetNewsAndLinksByUrlHandler(_mapper, _mockRepository.Object, _blobService.Object, _mockLogger.Object);
            string validUrl = "example1.com";
            var request = new GetNewsAndLinksByUrlQuery(validUrl);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.PrevNewsUrl.Should().BeNull();
        }

        [Fact]
        public async Task Handler_GetNewsAndLinksByValidUrl_NextNewsLinkShouldBeNull()
        {
            // Arrange
            var handler = new GetNewsAndLinksByUrlHandler(_mapper, _mockRepository.Object, _blobService.Object, _mockLogger.Object);
            string validUrl = "example3.com";
            var request = new GetNewsAndLinksByUrlQuery(validUrl);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.NextNewsUrl.Should().BeNull();
        }

        [Fact]
        public async Task Handler_GetNewsAndLinksByValidUrl_ResultShouldBeOfTypeNewsDtoWithURLs()
        {
            // Arrange
            var handler = new GetNewsAndLinksByUrlHandler(_mapper, _mockRepository.Object, _blobService.Object, _mockLogger.Object);
            string validUrl = "example2.com";
            var request = new GetNewsAndLinksByUrlQuery(validUrl);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().BeOfType<NewsDtoWithURLs>();
        }

        [Fact]
        public async Task Handler_GetNewsAndLinksByValidUrl_RandomNewsUrlShouldBeAsValidUrl()
        {
            // Arrange
            var handler = new GetNewsAndLinksByUrlHandler(_mapper, _mockRepository.Object, _blobService.Object, _mockLogger.Object);
            string validUrl = "example3.com";
            var request = new GetNewsAndLinksByUrlQuery(validUrl);
            string expected = "example1.com";

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.RandomNews?.RandomNewsUrl.Should().Be(expected);
        }
    }
}
