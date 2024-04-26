// <copyright file="UpdateAuthorShipHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.InfoBlocks.AuthorsInfoes.AuthorShips.Update
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.InfoBlocks.AuthorsInfoes;
    using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Update;
    using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class UpdateAuthorShipHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly IMapper _mapper;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _blobService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAuthorShipHandlerTest"/> class.
        /// </summary>
        public UpdateAuthorShipHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetAuthorShipRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AuthorShipProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();

            _blobService = new Mock<IBlobService>();
        }

        /// <summary>
        /// Update AuthorShipDto Is Null Is Failed Should Be True test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerAuthorShipDtoIsNullIsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateAuthorShipHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);

            AuthorShipDto? authorShipDto = null;

            var request = new UpdateAuthorShipCommand(authorShipDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        /// <summary>
        /// Update AuthorShipDto Is Valid Is Sucess Should Be True test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerAuthorShipDtoIsValidIsSucessShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateAuthorShipHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);

            AuthorShipDto? authorShipDto = new AuthorShipDto()
            {
                Id = 1,
                Text = "First Text",
                AuthorShipHyperLinkId = 1,
            };

            var request = new UpdateAuthorShipCommand(authorShipDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        /// <summary>
        /// Update AuthorShipDto Is Valid Update AuthorShip Is Called test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerAuthorShipDtoIsValidUpdateArticleIsCalled()
        {
            // Arrange
            var handler = new UpdateAuthorShipHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);

            AuthorShipDto? authorShipDto = new AuthorShipDto()
            {
                Id = 1,
                Text = "First Text",
                AuthorShipHyperLinkId = 1,
            };

            var request = new UpdateAuthorShipCommand(authorShipDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            _mockRepository.Verify(x => x.AuthorShipRepository.Update(It.IsAny<AuthorShip>()), Times.Once);
        }
    }
}
