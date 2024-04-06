// <copyright file="UpdateDictionaryItemHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.Dictionaries.Update
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.Dictionaries;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Dictionaries;
    using Streetcode.BLL.MediatR.Dictionaries.Update;
    using Streetcode.DAL.Entities.Dictionaries;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class UpdateDictionaryItemHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _blobService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDictionaryItemHandlerTest"/> class.
        /// </summary>
        public UpdateDictionaryItemHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetDictionaryItemRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<DictionaryItemProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();

            _blobService = new Mock<IBlobService>();
        }

        /// <summary>
        /// DictionaryItemDto IsNull IsFailed Should Be True update test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerDictionaryItemDtoIsNullIsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateDictionaryItemHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);

            DictionaryItemDto? dictionaryItemDto = null;

            var request = new UpdateDictionaryItemCommand(dictionaryItemDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        /// <summary>
        /// DictionaryItemDto IsValid IsSucess Should Be True update test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerDictionaryItemDtoIsValidIsSucessShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateDictionaryItemHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);

            DictionaryItemDto? dictionaryItemDto = new DictionaryItemDto()
            {
                Id = 1,
                Name = "First Name",
                Description = "First Description",
            };

            var request = new UpdateDictionaryItemCommand(dictionaryItemDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        /// <summary>
        /// DictionaryItemDto IsValid UpdateDictionaryItem IsCalled update test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerDictionaryItemDtoIsValidUpdateDictionaryItemIsCalled()
        {
            // Arrange
            var handler = new UpdateDictionaryItemHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);

            DictionaryItemDto? dictionaryItemDto = new DictionaryItemDto()
            {
                Id = 1,
                Name = "First Name",
                Description = "First Description",
            };

            var request = new UpdateDictionaryItemCommand(dictionaryItemDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            _mockRepository.Verify(x => x.DictionaryItemRepository.Update(It.IsAny<DictionaryItem>()), Times.Once);
        }
    }
}
