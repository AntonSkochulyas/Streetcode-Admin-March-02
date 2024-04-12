namespace Streetcode.XUnitTest.MediatRTests.Newss.Create
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.News;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Newss;
    using Streetcode.BLL.MediatR.Newss.Create;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    public class CreateNewsHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public CreateNewsHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetNewsRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<NewsProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        [Fact]
        public async Task Handle_NewsDtoIsNull_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new CreateNewsHandler(_mapper, _mockRepository.Object, _mockLogger.Object);
            NewsDto? newsDto = null;
            var streetcodeCoordinate = new CreateNewsCommand(newsDto);

            // Act
            var result = await handler.Handle(streetcodeCoordinate, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ValidDto_IsSuccessShouldBeTrue()
        {
            // Arrange
            var handler = new CreateNewsHandler(_mapper, _mockRepository.Object, _mockLogger.Object);
            NewsDto? newsDto = new NewsDto()
            {
                Id = 1,
                Text = "Text1",
                ImageId = 1,
                CreationDate = DateTime.Now,
                Title = "Title1",
                URL = "example.com",
            };
            var streetcodeCoordinate = new CreateNewsCommand(newsDto);

            // Act
            var result = await handler.Handle(streetcodeCoordinate, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
