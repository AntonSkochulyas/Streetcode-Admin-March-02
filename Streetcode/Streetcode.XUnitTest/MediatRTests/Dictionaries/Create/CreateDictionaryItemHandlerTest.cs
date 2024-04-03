// <copyright file="CreateDictionaryItemHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.Dictionaries.Create
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.Dictionaries;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Dictionaries;
    using Streetcode.BLL.MediatR.Dictionaries.Create;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.MediatRTests.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class CreateDictionaryItemHandlerTest
    {
        private readonly IMapper mapper;
        private readonly Mock<IRepositoryWrapper> mockRepository;
        private readonly Mock<ILoggerService> mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDictionaryItemHandlerTest"/> class.
        /// </summary>
        public CreateDictionaryItemHandlerTest()
        {
            this.mockRepository = RepositoryMocker.GetDictionaryItemMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<DictionaryItemProfile>();
            });

            this.mapper = mapperConfig.CreateMapper();

            this.mockLogger = new Mock<ILoggerService>();
        }

        /// <summary>
        /// Is Null IsFailed Should Be True created test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandleDictionaryItemDtoIsNullIsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new CreateDictionaryItemHandler(this.mapper, this.mockRepository.Object, this.mockLogger.Object);

            DictionaryItemDto? dictionaryItemDto = null;

            var newDictionaryItem = new CreateDictionaryItemCommand(dictionaryItemDto);

            // Act
            var result = await handler.Handle(newDictionaryItem, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        /// <summary>
        /// Valid Dto Is Success Should BeTrue created test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandleValidDtoIsSuccessShouldBeTrue()
        {
            // Arrange
            var handler = new CreateDictionaryItemHandler(this.mapper, this.mockRepository.Object, this.mockLogger.Object);

            DictionaryItemDto? dictionaryItemDto = new DictionaryItemDto()
            {
                Id = 1,
                Name = "First Name",
                Description = "First Description",
            };

            var newDictionaryItem = new CreateDictionaryItemCommand(dictionaryItemDto);

            // Act
            var result = await handler.Handle(newDictionaryItem, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
