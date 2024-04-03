// <copyright file="CreateAuthorShipHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.InfoBlocks.AuthorsInfoes.AuthorShips.Create
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.InfoBlocks.AuthorsInfoes;
    using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Create;
    using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.MediatRTests.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class CreateAuthorShipHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAuthorShipHandlerTest"/> class.
        /// </summary>
        public CreateAuthorShipHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetAuthorShipRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AuthorShipProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        /// <summary>
        /// Create Is Null Is Failed Should Be True test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandleAuthorShipDtoIsNullIsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new CreateAuthorShipHandler(_mapper, _mockRepository.Object, _mockLogger.Object);

            AuthorShipDto? authorShipDto = null;

            var newAuthorShip = new CreateAuthorShipCommand(authorShipDto);

            // Act
            var result = await handler.Handle(newAuthorShip, CancellationToken.None);

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
            var handler = new CreateAuthorShipHandler(_mapper, _mockRepository.Object, _mockLogger.Object);

            AuthorShipDto? authorShipDto = new AuthorShipDto()
            {
                Id = 1,
                Text = "First Text",
                AuthorShipHyperLinkId = 1,
                AuthorShipHyperLink = new AuthorShipHyperLink
                {
                    Id = 1,
                    Title = "First Title",
                    URL = "First URL",
                },
            };

            var newAuthorShip = new CreateAuthorShipCommand(authorShipDto);

            // Act
            var result = await handler.Handle(newAuthorShip, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
