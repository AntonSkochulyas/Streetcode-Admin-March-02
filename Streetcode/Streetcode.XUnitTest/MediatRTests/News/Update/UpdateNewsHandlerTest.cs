namespace Streetcode.XUnitTest.MediatRTests.News.Update
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.News;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Newss;
    using Streetcode.BLL.MediatR.Newss.Update;
    using Streetcode.DAL.Entities.Media.Images;
    using Streetcode.DAL.Entities.News;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    public class UpdateNewsHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _blobService;

        public UpdateNewsHandlerTest()
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
        public async Task Handler_NewsDtoIsNull_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateNewsHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);
            NewsDto? newsDto = null;
            var request = new UpdateNewsCommand(newsDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_NewsDtoIsValid_IsSucessShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateNewsHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);
            NewsDto? newsDto = new NewsDto()
            {
                Id = 1,
                Title = "Title1",
                Text = "Text1",
                CreationDate = new DateTime(2024, 3, 22, 0, 0, 0, DateTimeKind.Utc),
                ImageId = 1,
                URL = "example1.com",
            };
            var request = new UpdateNewsCommand(newsDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_NewsDtoIsValid_DeleteImageIsCalled()
        {
            // Arrange
            var handler = new UpdateNewsHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);
            NewsDto? newsDto = new NewsDto()
            {
                Id = 1,
                Title = "Title1",
                Text = "Text1",
                CreationDate = new DateTime(2024, 3, 22, 0, 0, 0, DateTimeKind.Utc),
                ImageId = 1,
                URL = "example1.com",
            };
            var request = new UpdateNewsCommand(newsDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            _mockRepository.Verify(x => x.ImageRepository.Delete(It.IsAny<Image>()), Times.Once);
        }

        [Fact]
        public async Task Handler_NewsDtoIsValid_UpdateNewsIsCalled()
        {
            // Arrange
            var handler = new UpdateNewsHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);
            NewsDto? newsDto = new NewsDto()
            {
                Id = 1,
                Title = "Title1",
                Text = "Text1",
                CreationDate = new DateTime(2024, 3, 22, 0, 0, 0, DateTimeKind.Utc),
                ImageId = 1,
                URL = "example1.com",
            };
            var request = new UpdateNewsCommand(newsDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            _mockRepository.Verify(x => x.NewsRepository.Update(It.IsAny<News>()), Times.Once);
        }
    }
}
