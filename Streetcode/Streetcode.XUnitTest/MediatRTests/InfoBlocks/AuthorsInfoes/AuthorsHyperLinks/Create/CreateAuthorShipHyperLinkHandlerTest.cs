// <copyright file="CreateAuthorShipHyperLinkHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Create
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
    using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Create;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class CreateAuthorShipHyperLinkHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAuthorShipHyperLinkHandlerTest"/> class.
        /// </summary>
        public CreateAuthorShipHyperLinkHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetAuthorShipHyperLinkRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AuthorShipHyperLinkProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        /// <summary>
        /// Create IsNull IsFailed Should Be True test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandleAuthorShipHyperLinkDtoIsNullIsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new CreateAuthorShipHyperLinkHandler(_mapper, _mockRepository.Object, _mockLogger.Object);

            AuthorShipHyperLinkDto? authorShipHyperLinkDto = null;

            var newArticle = new CreateAuthorShipHyperLinkCommand(authorShipHyperLinkDto);

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
            var handler = new CreateAuthorShipHyperLinkHandler(_mapper, _mockRepository.Object, _mockLogger.Object);

            AuthorShipHyperLinkDto? authorShipHyperLinkDto = new AuthorShipHyperLinkDto()
            {
                Id = 1,
                Title = "First Title",
                URL = "First URL",
            };

            var newArticle = new CreateAuthorShipHyperLinkCommand(authorShipHyperLinkDto);

            // Act
            var result = await handler.Handle(newArticle, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
