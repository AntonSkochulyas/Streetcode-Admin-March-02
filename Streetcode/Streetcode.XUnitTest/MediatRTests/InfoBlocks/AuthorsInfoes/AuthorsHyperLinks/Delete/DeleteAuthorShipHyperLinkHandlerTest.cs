// <copyright file="DeleteAuthorShipHyperLinkHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Delete
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.InfoBlocks.AuthorsInfoes;
    using Streetcode.BLL.Mapping.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
    using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Delete;
    using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class DeleteAuthorShipHyperLinkHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAuthorShipHyperLinkHandlerTest"/> class.
        /// </summary>
        public DeleteAuthorShipHyperLinkHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetAuthorShipHyperLinkRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AuthorShipHyperLinkProfile>();
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
            var handler = new DeleteAuthorShipHyperLinkHandler(_mockRepository.Object, _mockLogger.Object, _mapper);

            int wrongId = 10;
            var request = new DeleteAuthorShipHyperLinkCommand(wrongId);

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
            var handler = new DeleteAuthorShipHyperLinkHandler(_mockRepository.Object, _mockLogger.Object, _mapper);

            int correctId = 1;
            var request = new DeleteAuthorShipHyperLinkCommand(correctId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            _mockRepository.Verify(x => x.AuthorShipHyperLinkRepository.Delete(It.IsAny<AuthorShipHyperLink>()), Times.Once);
        }
    }
}
