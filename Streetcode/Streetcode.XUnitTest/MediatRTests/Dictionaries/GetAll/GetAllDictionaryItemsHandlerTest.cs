// <copyright file="GetAllDictionaryItemsHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.Dictionaries.GetAll
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.Dictionaries;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Dictionaries;
    using Streetcode.BLL.MediatR.Dictionaries.GetAll;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.MediatRTests.Mocks;
    using Xunit;

    /// <summary>
    /// TESTED SUCCESSFULLY.
    /// </summary>
    public class GetAllDictionaryItemsHandlerTest
    {
        private readonly IMapper mapper;
        private readonly Mock<IRepositoryWrapper> mockRepository;
        private readonly Mock<ILoggerService> mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllDictionaryItemsHandlerTest"/> class.
        /// </summary>
        public GetAllDictionaryItemsHandlerTest()
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
        /// Get all not null or empty test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerGetAllResultShouldNotBeNullOrEmpty()
        {
            // Arrange
            var handler = new GetAllDictionaryItemsHandler(this.mockRepository.Object, this.mapper, this.mockLogger.Object);
            var request = new GetAllDictionaryItemsQuery();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNullOrEmpty();
        }

        /// <summary>
        /// Get all list should be type DictionaryItemDTO.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetAllShouldBeTypeListDictionaryItemDTO()
        {
            // Arrange
            var handler = new GetAllDictionaryItemsHandler(this.mockRepository.Object, this.mapper, this.mockLogger.Object);
            var request = new GetAllDictionaryItemsQuery();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().BeOfType<List<DictionaryItemDto>>();
        }

        /// <summary>
        /// Get all list count shoul be four.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerGetAllCountShouldBeFour()
        {
            // Arrange
            var handler = new GetAllDictionaryItemsHandler(this.mockRepository.Object, this.mapper, this.mockLogger.Object);
            var request = new GetAllDictionaryItemsQuery();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Count().Should().Be(4);
        }
    }
}
