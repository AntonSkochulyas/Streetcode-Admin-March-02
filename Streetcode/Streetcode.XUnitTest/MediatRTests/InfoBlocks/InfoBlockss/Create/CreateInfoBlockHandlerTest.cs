// <copyright file="CreateInfoBlockHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.InfoBlocks.InfoBlockss.Create
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.InfoBlocks;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.InfoBlocks;
    using Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Create;
    using Streetcode.DAL.Entities.InfoBlocks.Articles;
    using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;
    using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.MediatRTests.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class CreateInfoBlockHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateInfoBlockHandlerTest"/> class.
        /// </summary>
        public CreateInfoBlockHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetInfoBlockRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<InfoBlockProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        /// <summary>
        /// Create Is Null Is Failed Should Be True test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandleArticleDtoIsNullIsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new CreateInfoBlockHandler(_mapper, _mockRepository.Object, _mockLogger.Object);

            InfoBlockDto? infoBlockeDto = null;

            var newInfoBlock = new CreateInfoBlockCommand(infoBlockeDto);

            // Act
            var result = await handler.Handle(newInfoBlock, CancellationToken.None);

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
            var handler = new CreateInfoBlockHandler(_mapper, _mockRepository.Object, _mockLogger.Object);

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
                    AuthorShipHyperLink = new AuthorShipHyperLink { Id = 1, Title = "First Title", URL = "First URL" },
                },
            };

            var newInfoBlock = new CreateInfoBlockCommand(infoBlockDto);

            // Act
            var result = await handler.Handle(newInfoBlock, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
