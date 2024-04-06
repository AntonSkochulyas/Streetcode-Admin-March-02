// <copyright file="UpdateArticleHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.InfoBlocks.Articles.Update
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.InfoBlocks.Articles;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.InfoBlocks.Articles;
    using Streetcode.BLL.MediatR.InfoBlocks.Articles.Update;
    using Streetcode.DAL.Entities.InfoBlocks.Articles;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class UpdateArticleHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly IMapper _mapper;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _blobService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateArticleHandlerTest"/> class.
        /// </summary>
        public UpdateArticleHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetArticleRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ArticleProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();

            _blobService = new Mock<IBlobService>();
        }

        /// <summary>
        /// Update ArticleDto Is Null Is Failed Should Be True test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerArticleDtoIsNullIsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateArticleHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);

            ArticleDto? articleDto = null;

            var request = new UpdateArticleCommand(articleDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        /// <summary>
        /// Update ArticleDto Is Valid Is Sucess Should Be True test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerArticleDtoIsValidIsSucessShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateArticleHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);

            ArticleDto? articleDto = new ArticleDto()
            {
                Id = 1,
                Text = "First Text",
                Title = "First Title",
            };

            var request = new UpdateArticleCommand(articleDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        /// <summary>
        /// Update ArticleDto Is Valid Update Article Is Called test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerArticleDtoIsValidUpdateArticleIsCalled()
        {
            // Arrange
            var handler = new UpdateArticleHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);

            ArticleDto? articleDto = new ArticleDto()
            {
                Id = 1,
                Text = "First Text",
                Title = "First Title",
            };

            var request = new UpdateArticleCommand(articleDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            _mockRepository.Verify(x => x.ArticleRepository.Update(It.IsAny<Article>()), Times.Once);
        }
    }
}
