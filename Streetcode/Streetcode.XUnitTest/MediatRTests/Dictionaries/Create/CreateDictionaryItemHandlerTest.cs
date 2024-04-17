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
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class CreateDictionaryItemHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDictionaryItemHandlerTest"/> class.
        /// </summary>
        public CreateDictionaryItemHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetDictionaryItemRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<DictionaryItemProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        /// <summary>
        /// Is Null IsFailed Should Be True created test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandleDictionaryItemDtoIsNullIsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new CreateDictionaryItemHandler(_mapper, _mockRepository.Object, _mockLogger.Object);

            CreateDictionaryItemDto? dictionaryItemDto = null;

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
            var handler = new CreateDictionaryItemHandler(_mapper, _mockRepository.Object, _mockLogger.Object);

            CreateDictionaryItemDto? dictionaryItemDto = new CreateDictionaryItemDto()
            {
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
