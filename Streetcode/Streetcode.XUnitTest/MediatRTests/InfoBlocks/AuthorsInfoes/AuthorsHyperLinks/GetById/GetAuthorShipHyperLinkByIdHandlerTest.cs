// <copyright file="GetAuthorShipHyperLinkByIdHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.InfoBlocks.Articles;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.GetById;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.GetAll;
using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.GetById;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.GetById
{
    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class GetAuthorShipHyperLinkByIdHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAuthorShipHyperLinkByIdHandlerTest"/> class.
        /// </summary>
        public GetAuthorShipHyperLinkByIdHandlerTest()
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
        /// Get AuthorShipHyperLink By Valid Id Result Should Be Not Null test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerGetAuthorShipHyperLinkByValidIdResultShouldBeNotNull()
        {
            // Arrange
            var handler = new GetAuthorShipHyperLinksByIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            int validId = 1;
            var request = new GetAuthorShipHyperLinksByIdQuery(validId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }

        /// <summary>
        /// Get AuthorShipHyperLink By Invalid Id Is Failed Should Be True test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerGetAuthorShipHyperLinkByInvalidIdIsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new GetAuthorShipHyperLinksByIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            int invalidId = 10;
            var request = new GetAuthorShipHyperLinksByIdQuery(invalidId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        /// <summary>
        /// Get AuthorShipHyperLink By Valid Id Result Should BeType Of AuthorShipHyperLinkDto test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerGetAuthorShipHyperLinkByValidIdResultShouldBeTypeOfAuthorShipHyperLinkDto()
        {
            // Arrange
            var handler = new GetAuthorShipHyperLinksByIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            int validId = 1;
            var request = new GetAuthorShipHyperLinksByIdQuery(validId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().BeOfType<AuthorShipHyperLinkDto>();
        }
    }
}
