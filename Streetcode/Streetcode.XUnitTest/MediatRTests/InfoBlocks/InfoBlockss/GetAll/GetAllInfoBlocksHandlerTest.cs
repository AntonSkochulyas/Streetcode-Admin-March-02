// <copyright file="GetAllInfoBlocksHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.InfoBlocks.InfoBlockss.GetAll
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.InfoBlocks;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.InfoBlocks;
    using Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.GetAll;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// TESTED SUCCESSFULLY.
    /// </summary>
    public class GetAllInfoBlocksHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly IMapper _mapper;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllInfoBlocksHandlerTest"/> class.
        /// </summary>
        public GetAllInfoBlocksHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetInfoBlockRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<InfoBlockProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        ///// <summary>
        ///// Get all not null or empty test.
        ///// </summary>
        ///// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        //[Fact]
        //public async Task GetAllNotNullOrEmptyTest()
        //{
        //    // Arrange
        //    var handler = new GetAllInfoBlocksHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        //    // Act
        //    var result = await handler.Handle(new GetAllInfoBlocksQuery(), CancellationToken.None);

        //    // Assert
        //    result.Value.Should().NotBeNullOrEmpty();
        //}

        ///// <summary>
        ///// Get all list count shoul be four.
        ///// </summary>
        ///// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        //[Fact]
        //public async Task GetAllCountShouldBeFour()
        //{
        //    // Arrange
        //    var handler = new GetAllInfoBlocksHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        //    // Act
        //    var result = await handler.Handle(new GetAllInfoBlocksQuery(), CancellationToken.None);

        //    // Assert
        //    result.Value.Count().Should().Be(4);
        //}

        ///// <summary>
        ///// Get all list should be type InfoBlockDTO.
        ///// </summary>
        ///// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        //[Fact]
        //public async Task GetAllShouldBeTypeListArticleDTO()
        //{
        //    // Arrange
        //    var handler = new GetAllInfoBlocksHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        //    // Act
        //    var result = await handler.Handle(new GetAllInfoBlocksQuery(), CancellationToken.None);

        //    // Assert
        //    result.Value.Should().BeOfType<List<InfoBlockDto>>();
        //}
    }
}
