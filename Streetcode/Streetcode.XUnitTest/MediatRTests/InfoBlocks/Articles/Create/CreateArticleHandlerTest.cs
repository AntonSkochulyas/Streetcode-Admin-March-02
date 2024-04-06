// <copyright file="CreateArticleHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.InfoBlocks.Articles.Create
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.InfoBlocks.Articles;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.InfoBlocks.Articles;
    using Streetcode.BLL.MediatR.InfoBlocks.Articles.Create;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class CreateArticleHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateArticleHandlerTest"/> class.
        /// </summary>
        public CreateArticleHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetArticleRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ArticleProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        /// <summary>
        /// Create IsNull IsFailed Should Be True test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandleArticleDtoIsNullIsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new CreateArticleHandler(_mapper, _mockRepository.Object, _mockLogger.Object);

            ArticleDto? articleDto = null;

            var newArticle = new CreateArticleCommand(articleDto);

            // Act
            var result = await handler.Handle(newArticle, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        /// <summary>
        /// Create ValidDto IsSuccess Should Be True test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandleValidDtoIsSuccessShouldBeTrue()
        {
            // Arrange
            var handler = new CreateArticleHandler(_mapper, _mockRepository.Object, _mockLogger.Object);

            ArticleDto? articleDto = new ArticleDto()
            {
                Id = 1,
                Title = "First Title",
                Text = "First Text",
            };

            var newArticle = new CreateArticleCommand(articleDto);

            // Act
            var result = await handler.Handle(newArticle, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
