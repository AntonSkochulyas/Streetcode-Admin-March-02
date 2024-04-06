// <copyright file="DeleteImageHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.Media.Images.Delete
{
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.MediatR.Media.Image.Delete;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Can not test.
    /// </summary>
    public class DeleteImageHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _mockBlob;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteImageHandlerTest"/> class.
        /// </summary>
        public DeleteImageHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetImagesRepositoryMock();

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
            var handler = new DeleteImageHandler(_mockRepository.Object, _mockBlob.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new DeleteImageCommand(1), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }
    }
}
