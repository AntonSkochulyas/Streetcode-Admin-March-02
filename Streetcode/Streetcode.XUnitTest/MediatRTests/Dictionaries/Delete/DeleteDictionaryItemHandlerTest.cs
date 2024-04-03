// <copyright file="DeleteDictionaryItemHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.Dictionaries.Delete
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Dictionaries;
    using Streetcode.BLL.MediatR.Dictionaries.Delete;
    using Streetcode.DAL.Entities.Dictionaries;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.MediatRTests.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class DeleteDictionaryItemHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> mockRepository;
        private readonly Mock<ILoggerService> mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteDictionaryItemHandlerTest"/> class.
        /// </summary>
        public DeleteDictionaryItemHandlerTest()
        {
            this.mockRepository = RepositoryMocker.GetDictionaryItemMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<DictionaryItemProfile>();
            });

            this.mockLogger = new Mock<ILoggerService>();
        }

        /// <summary>
        /// Delete item with wrong id result should be true test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerWrongIdIsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new DeleteDictionaryItemHandler(this.mockRepository.Object, this.mockLogger.Object);

            int wrongId = 10;
            var request = new DeleteDictionaryItemCommand(wrongId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        /// <summary>
        /// Delete item with correct first id result should be called test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerCorrectIdDeleteShouldBeCalled()
        {
            // Arrange
            var handler = new DeleteDictionaryItemHandler(this.mockRepository.Object, this.mockLogger.Object);

            int correctId = 1;
            var request = new DeleteDictionaryItemCommand(correctId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            this.mockRepository.Verify(x => x.DictionaryItemRepository.Delete(It.IsAny<DictionaryItem>()), Times.Once);
        }
    }
}
