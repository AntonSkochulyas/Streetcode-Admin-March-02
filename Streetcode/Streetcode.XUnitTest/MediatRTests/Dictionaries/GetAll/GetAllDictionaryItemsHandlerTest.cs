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
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllDictionaryItemsHandlerTest"/> class.
        /// </summary>
        public GetAllDictionaryItemsHandlerTest()
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
        /// Get all not null or empty test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerGetAllResultShouldNotBeNullOrEmpty()
        {
            // Arrange
            var handler = new GetAllDictionaryItemsHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
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
            var handler = new GetAllDictionaryItemsHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
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
            var handler = new GetAllDictionaryItemsHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            var request = new GetAllDictionaryItemsQuery();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Count().Should().Be(4);
        }
    }
}
