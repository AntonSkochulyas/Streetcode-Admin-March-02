// <copyright file="GetBaseAudioHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.Media.Audio.GetBaseAudio
{
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Media.Audio.GetBaseAudio;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// CAN NOT TEST
    /// BLL -> MediatR -> Media -> Audio -> GetBaseAudio.
    /// </summary>
    public class GetBaseAudioHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _mockBlob;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetBaseAudioHandlerTest"/> class.
        /// </summary>
        public GetBaseAudioHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetAudiosRepositoryMock();

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
            MemoryStream memory = new MemoryStream();
            _mockBlob.Setup(x => x.FindFileInStorageAsMemoryStream(It.IsAny<string>())).Returns(memory);
            var handler = new GetBaseAudioHandler(_mockBlob.Object, _mockRepository.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetBaseAudioQuery(1), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }
    }
}
