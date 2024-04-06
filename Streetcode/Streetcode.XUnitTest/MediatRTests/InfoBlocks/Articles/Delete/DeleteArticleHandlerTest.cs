// <copyright file="DeleteArticleHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.InfoBlocks.Articles.Delete
{
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.InfoBlocks.Articles.Delete;
    using Streetcode.DAL.Entities.InfoBlocks.Articles;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class DeleteArticleHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteArticleHandlerTest"/> class.
        /// </summary>
        public DeleteArticleHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetArticleRepositoryMock();
            _mockLogger = new Mock<ILoggerService>();
        }

        /// <summary>
        /// Delete WrongId IsFailed Should Be True test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerWrongIdIsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new DeleteArticleHandler(_mockRepository.Object, _mockLogger.Object);

            int wrongId = 10;
            var request = new DeleteArticleCommand(wrongId);

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
            var handler = new DeleteArticleHandler(_mockRepository.Object, _mockLogger.Object);

            int correctId = 1;
            var request = new DeleteArticleCommand(correctId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            _mockRepository.Verify(x => x.ArticleRepository.Delete(It.IsAny<Article>()), Times.Once);
        }
    }
}
