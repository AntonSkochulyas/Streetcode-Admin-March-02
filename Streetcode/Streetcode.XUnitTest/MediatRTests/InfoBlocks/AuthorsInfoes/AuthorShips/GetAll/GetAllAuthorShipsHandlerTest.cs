﻿// <copyright file="GetAllAuthorShipsHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.InfoBlocks.AuthorsInfoes.AuthorShips.GetAll
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.InfoBlocks.AuthorsInfoes;
    using Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.GetAll;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.MediatRTests.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class GetAllAuthorShipsHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> mockRepository;
        private readonly IMapper mapper;
        private readonly Mock<ILoggerService> mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllAuthorShipsHandlerTest"/> class.
        /// </summary>
        public GetAllAuthorShipsHandlerTest()
        {
            this.mockRepository = RepositoryMocker.GetAuthorShipRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<AuthorShipProfile>();
            });

            this.mapper = mapperConfig.CreateMapper();

            this.mockLogger = new Mock<ILoggerService>();
        }

        /// <summary>
        /// Get all not null or empty test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetAllNotNullOrEmptyTest()
        {
            // Arrange
            var handler = new GetAllAuthorShipsHandler(this.mockRepository.Object, this.mapper, this.mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetAllAuthorShipsQuery(), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNullOrEmpty();
        }

        /// <summary>
        /// Get all list count shoul be four.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetAllCountShouldBeFour()
        {
            // Arrange
            var handler = new GetAllAuthorShipsHandler(this.mockRepository.Object, this.mapper, this.mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetAllAuthorShipsQuery(), CancellationToken.None);

            // Assert
            result.Value.Count().Should().Be(4);
        }

        /// <summary>
        /// Get all list should be type ArticleDTO.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetAllShouldBeTypeListArticleDTO()
        {
            // Arrange
            var handler = new GetAllAuthorShipsHandler(this.mockRepository.Object, this.mapper, this.mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetAllAuthorShipsQuery(), CancellationToken.None);

            // Assert
            result.Value.Should().BeOfType<List<AuthorShipDto>>();
        }
    }
}
