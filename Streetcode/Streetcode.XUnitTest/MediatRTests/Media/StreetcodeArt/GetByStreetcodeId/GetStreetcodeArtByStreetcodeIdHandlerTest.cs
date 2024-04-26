using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Media.StreetcodeArt.GetByStreetcodeId;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Media.StreetcodeArt.GetByStreetcodeId
{
    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class GetStreetcodeArtByStreetcodeIdHandlerTest
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _mockBlob;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetStreetcodeArtByStreetcodeIdHandlerTest"/> class.
        /// </summary>
        public GetStreetcodeArtByStreetcodeIdHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetStreetcodeArtRepositoryMock();

            _mockMapper = new Mock<IMapper>();

            _mockLogger = new Mock<ILoggerService>();

            _mockBlob = new Mock<IBlobService>();
        }

        /// <summary>
        /// Get by id not null test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetByIdNotNullTest()
        {
            // Arrange
            var handler = new GetStreetcodeArtByStreetcodeIdHandler(_mockRepository.Object, _mockMapper.Object, _mockBlob.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetStreetcodeArtByStreetcodeIdQuery(1), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }

        /// <summary>
        /// Get by id collection size should be zero.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetByIdSizeShoulBeZero()
        {
            // Arrange
            var handler = new GetStreetcodeArtByStreetcodeIdHandler(_mockRepository.Object, _mockMapper.Object, _mockBlob.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetStreetcodeArtByStreetcodeIdQuery(1), CancellationToken.None);

            // Assert
            result.Value.Count().Should().Be(0);
        }
    }
}
