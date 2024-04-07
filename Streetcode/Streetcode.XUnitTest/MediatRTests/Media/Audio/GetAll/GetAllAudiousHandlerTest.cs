namespace Streetcode.XUnitTest.MediatRTests.Media.Audio.GetAll
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.Media.Audio;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Media;
    using Streetcode.BLL.MediatR.Media.Audio.GetAll;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// TESTED SUCCESSFULLY
    /// BLL -> MediatR -> Media -> Audio -> GetAll.
    /// </summary>
    public class GetAllAudiousHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _mockBlob;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllAudiousHandlerTest"/> class.
        /// </summary>
        public GetAllAudiousHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetAudiosRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AudioProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();

            _mockBlob = new Mock<IBlobService>();
        }

        /// <summary>
        /// Get all not null or empty test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetAllNotNullOrEmptyTest()
        {
            // Arrange
            var handler = new GetAllAudiosHandler(_mockRepository.Object, _mapper, _mockBlob.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetAllAudiosQuery(), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNullOrEmpty();
        }

        /// <summary>
        /// Get all list count shoul be four.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetAllCountShouldBeFour()
        {
            // Arrange
            var handler = new GetAllAudiosHandler(_mockRepository.Object, _mapper, _mockBlob.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetAllAudiosQuery(), CancellationToken.None);

            // Assert
            result.Value.Count().Should().Be(4);
        }

        /// <summary>
        /// Get all list should be type AudioDTO.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetAllShouldBeTypeListAudioDTO()
        {
            // Arrange
            var handler = new GetAllAudiosHandler(_mockRepository.Object, _mapper, _mockBlob.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetAllAudiosQuery(), CancellationToken.None);

            // Assert
            result.Value.Should().BeOfType<List<AudioDto>>();
        }
    }
}
