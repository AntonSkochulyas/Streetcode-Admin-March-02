namespace Streetcode.XUnitTest.MediatRTests.Media.Audio.Delete
{
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Media;
    using Streetcode.BLL.MediatR.Media.Audio.Delete;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// CAN NOT TEST.
    /// </summary>
    public class DeleteAudioHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _mockBlob;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAudioHandlerTest"/> class.
        /// </summary>
        public DeleteAudioHandlerTest()
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
        /// Delete item with first id result should not be null test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task DeleteWithFirstIdShouldNotBeNull()
        {
            // Arrange
            var handler = new DeleteAudioHandler(_mockRepository.Object, _mockBlob.Object, _mockLogger.Object, _mapper);

            // Act
            var result = await handler.Handle(new DeleteAudioCommand(1), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }
    }
}
