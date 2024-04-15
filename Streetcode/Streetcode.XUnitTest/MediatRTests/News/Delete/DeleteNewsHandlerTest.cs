namespace Streetcode.XUnitTest.MediatRTests.News.Delete
{
    using System.Threading.Tasks;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Newss.Delete;
    using Streetcode.DAL.Entities.News;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    public class DeleteNewsHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public DeleteNewsHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetNewsRepositoryMock();

            _mockLogger = new Mock<ILoggerService>();
        }

        [Fact]
        public async Task Handler_WrongId_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new DeleteNewsHandler(_mockRepository.Object, _mockLogger.Object);

            int wrongId = 10;
            var request = new DeleteNewsCommand(wrongId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_CorrectId_DeleteShouldBeCalled()
        {
            // Arrange
            var handler = new DeleteNewsHandler(_mockRepository.Object, _mockLogger.Object);

            int correctId = 1;
            var request = new DeleteNewsCommand(correctId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            _mockRepository.Verify(x => x.NewsRepository.Delete(It.IsAny<News>()), Times.Once);
        }
    }
}
