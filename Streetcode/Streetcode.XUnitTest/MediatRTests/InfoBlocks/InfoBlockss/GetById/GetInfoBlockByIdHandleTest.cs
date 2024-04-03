// <copyright file="GetInfoBlockByIdHandleTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.InfoBlocks.InfoBlockss.GetById
{
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.InfoBlocks;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.InfoBlocks;
    using Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.GetById;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.MediatRTests.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class GetInfoBlockByIdHandleTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetInfoBlockByIdHandleTest"/> class.
        /// </summary>
        public GetInfoBlockByIdHandleTest()
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
        /// Get InfoBlock By Valid Id Result Should Be Not Null test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        //[Fact]
        //public async Task HandlerGetInfoBlockByValidIdResultShouldBeNotNull()
        //{
        //    // Arrange
        //    var handler = new GetInfoBlockByIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
        //    int validId = 1;
        //    var request = new GetInfoBlockByIdQuery(validId);

        //    // Act
        //    var result = await handler.Handle(request, CancellationToken.None);

        //    // Assert
        //    result.Value.Should().NotBeNull();
        //}

        /// <summary>
        /// Get InfoBlock By Invalid Id Is Failed Should Be True test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task HandlerGetInfoBlockByInvalidIdIsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new GetInfoBlockByIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            int invalidId = 10;
            var request = new GetInfoBlockByIdQuery(invalidId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        /// <summary>
        /// Get InfoBlock By Valid Id Result Should BeType Of InfoBlockDto test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        //[Fact]
        //public async Task HandlerGetInfoBlockByValidIdResultShouldBeTypeOfArticleDto()
        //{
        //    // Arrange
        //    var handler = new GetInfoBlockByIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
        //    int validId = 1;
        //    var request = new GetInfoBlockByIdQuery(validId);

        //    // Act
        //    var result = await handler.Handle(request, CancellationToken.None);

        //    // Assert
        //    result.Value.Should().BeOfType<InfoBlockDto>();
        //}
    }
}
