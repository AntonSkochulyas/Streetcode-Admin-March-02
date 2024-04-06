// <copyright file="UpdateAuthorShipHyperLinkHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.InfoBlocks.Articles;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.Update;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Update;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Update
{
    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class UpdateAuthorShipHyperLinkHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly IMapper _mapper;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _blobService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAuthorShipHyperLinkHandlerTest"/> class.
        /// </summary>
        public UpdateAuthorShipHyperLinkHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetAuthorShipHyperLinkRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AuthorShipHyperLinkProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();

            _blobService = new Mock<IBlobService>();
        }

        /// <summary>
        /// Update AuthorShipHyperLinkDto Is Null Is Failed Should Be True test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerAuthorShipHyperLinkDtoIsNullIsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateAuthorShipHyperLinkHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);

            AuthorShipHyperLinkDto? authorShipHyperLinkDto = null;

            var request = new UpdateAuthorShipHyperLinkCommand(authorShipHyperLinkDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        /// <summary>
        /// Update AuthorShipHyperLinkDto Is Valid Is Sucess Should Be True test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerAuthorShipHyperLinkDtoIsValidIsSucessShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateAuthorShipHyperLinkHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);

            AuthorShipHyperLinkDto? authorShipHyperLinkDto = new AuthorShipHyperLinkDto()
            {
                Id = 1,
                Title = "First Title",
                URL = "First URL",
            };

            var request = new UpdateAuthorShipHyperLinkCommand(authorShipHyperLinkDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        /// <summary>
        /// Update AuthorShipHyperLinkDto Is Valid Update AuthorShipHyperLink Is Called test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerAuthorShipHyperLinkDtoIsValidUpdateArticleIsCalled()
        {
            // Arrange
            var handler = new UpdateAuthorShipHyperLinkHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);

            AuthorShipHyperLinkDto? authorShipHyperLinkDto = new AuthorShipHyperLinkDto()
            {
                Id = 1,
                Title = "First Title",
                URL = "First URL",
            };

            var request = new UpdateAuthorShipHyperLinkCommand(authorShipHyperLinkDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            _mockRepository.Verify(x => x.AuthorShipHyperLinkRepository.Update(It.IsAny<AuthorShipHyperLink>()), Times.Once);
        }
    }
}
