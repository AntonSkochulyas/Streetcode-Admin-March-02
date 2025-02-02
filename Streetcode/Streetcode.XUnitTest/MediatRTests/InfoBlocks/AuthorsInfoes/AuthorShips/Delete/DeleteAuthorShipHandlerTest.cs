namespace Streetcode.XUnitTest.MediatRTests.InfoBlocks.AuthorsInfoes.AuthorShips.Delete
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.InfoBlocks.AuthorsInfoes;
    using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Delete;
    using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class DeleteAuthorShipHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAuthorShipHandlerTest"/> class.
        /// </summary>
        public DeleteAuthorShipHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetAuthorShipRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AuthorShipProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

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
            var handler = new DeleteAuthorShipHandler(_mockRepository.Object, _mockLogger.Object, _mapper);

            int wrongId = 10;
            var request = new DeleteAuthorShipCommand(wrongId);

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
            var handler = new DeleteAuthorShipHandler(_mockRepository.Object, _mockLogger.Object, _mapper);

            int correctId = 1;
            var request = new DeleteAuthorShipCommand(correctId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            _mockRepository.Verify(x => x.AuthorShipRepository.Delete(It.IsAny<AuthorShip>()), Times.Once);
        }
    }
}
