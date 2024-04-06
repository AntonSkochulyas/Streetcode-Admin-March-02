// <copyright file="DeleteInfoBlockHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.InfoBlocks.InfoBlockss.Delete
{
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Delete;
    using Streetcode.DAL.Entities.InfoBlocks;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class DeleteInfoBlockHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteInfoBlockHandlerTest"/> class.
        /// </summary>
        public DeleteInfoBlockHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetInfoBlockRepositoryMock();
            _mockLogger = new Mock<ILoggerService>();
        }

        /// <summary>
        /// Delete Wrong Id Is Failed Should Be True test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerWrongIdIsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new DeleteInfoBlockHandler(_mockRepository.Object, _mockLogger.Object);

            int wrongId = 10;
            var request = new DeleteInfoBlockCommand(wrongId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        /// <summary>
        /// Correct Id Delete Should Be Called test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerCorrectIdDeleteShouldBeCalled()
        {
            // Arrange
            var handler = new DeleteInfoBlockHandler(_mockRepository.Object, _mockLogger.Object);

            int correctId = 1;
            var request = new DeleteInfoBlockCommand(correctId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            _mockRepository.Verify(x => x.InfoBlockRepository.Delete(It.IsAny<InfoBlock>()), Times.Once);
        }
    }
}
