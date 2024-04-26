// <copyright file="UpdateInfoBlockHandleTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.InfoBlocks.InfoBlockss.Update
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.InfoBlocks;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.InfoBlocks;
    using Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Update;
    using Streetcode.DAL.Entities.InfoBlocks;
    using Streetcode.DAL.Entities.InfoBlocks.Articles;
    using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class UpdateInfoBlockHandleTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly IMapper _mapper;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _blobService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateInfoBlockHandleTest"/> class.
        /// </summary>
        public UpdateInfoBlockHandleTest()
        {
            _mockRepository = RepositoryMocker.GetInfoBlockRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<InfoBlockProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();

            _blobService = new Mock<IBlobService>();
        }

        /// <summary>
        /// Update InfoBlockDto Is Null Is Failed Should Be True test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerInfoBlockDtoIsNullIsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateInfoBlockHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);

            InfoBlockDto? infoBlockDto = null;

            var request = new UpdateInfoBlockCommand(infoBlockDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        /// <summary>
        /// Update InfoBlockDto Is Valid Is Sucess Should Be True test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerInfoBlockDtoIsValidIsSucessShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateInfoBlockHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);

            InfoBlockDto? infoBlockDto = new InfoBlockDto()
            {
                Id = 1,
                ArticleId = 1,
                Article = new Article { Id = 1, Text = "First Text", Title = "First Title" },
                VideoURL = "www.youtube.com",
                AuthorShipId = 1,
                AuthorShip = new AuthorShip
                {
                    Id = 1,
                    Text = "First Text",
                    AuthorShipHyperLinkId = 1,
                },
            };

            var request = new UpdateInfoBlockCommand(infoBlockDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        /// <summary>
        /// Update InfoBlockDto Is Valid Update Article Is Called test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerInfoBlockDtoIsValidUpdateArticleIsCalled()
        {
            // Arrange
            var handler = new UpdateInfoBlockHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);

            InfoBlockDto? infoBlockDto = new InfoBlockDto()
            {
                Id = 1,
                ArticleId = 1,
                Article = new Article { Id = 1, Text = "First Text", Title = "First Title" },
                VideoURL = "www.youtube.com",
                AuthorShipId = 1,
                AuthorShip = new AuthorShip
                {
                    Id = 1,
                    Text = "First Text",
                    AuthorShipHyperLinkId = 1,
                },
            };

            var request = new UpdateInfoBlockCommand(infoBlockDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            _mockRepository.Verify(x => x.InfoBlockRepository.Update(It.IsAny<InfoBlock>()), Times.Once);
        }
    }
}
