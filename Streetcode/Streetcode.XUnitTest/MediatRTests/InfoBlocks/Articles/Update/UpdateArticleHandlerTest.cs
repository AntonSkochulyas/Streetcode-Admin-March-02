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
    using Streetcode.XUnitTest.MediatRTests.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class UpdateArticleHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> mockRepository;
        private readonly IMapper mapper;
        private readonly Mock<ILoggerService> mockLogger;
        private readonly Mock<IBlobService> blobService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateArticleHandlerTest"/> class.
        /// </summary>
        public UpdateArticleHandlerTest()
        {
            this.mockRepository = RepositoryMocker.GetArticleRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ArticleProfile>();
            });

            this.mapper = mapperConfig.CreateMapper();

            this.mockLogger = new Mock<ILoggerService>();

            this.blobService = new Mock<IBlobService>();
        }

        /// <summary>
        /// Update ArticleDto Is Null Is Failed Should Be True test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerArticleDtoIsNullIsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new UpdateArticleHandler(this.mockRepository.Object, this.mapper, this.blobService.Object, this.mockLogger.Object);

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
            var handler = new UpdateArticleHandler(this.mockRepository.Object, this.mapper, this.blobService.Object, this.mockLogger.Object);

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
            var handler = new UpdateArticleHandler(this.mockRepository.Object, this.mapper, this.blobService.Object, this.mockLogger.Object);

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
            this.mockRepository.Verify(x => x.ArticleRepository.Update(It.IsAny<Article>()), Times.Once);
        }
    }
}
