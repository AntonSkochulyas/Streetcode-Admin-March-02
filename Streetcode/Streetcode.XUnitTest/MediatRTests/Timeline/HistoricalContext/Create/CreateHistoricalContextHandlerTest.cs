// <copyright file="CreateHistoricalContextHandlerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Streetcode.XUnitTest.MediatRTests.Timeline.HistoricalContext.Create
{
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Timeline;
    using Streetcode.BLL.MediatR.Timeline.HistoricalContext.Create;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Can not test.
    /// </summary>
    public class CreateHistoricalContextHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateHistoricalContextHandlerTest"/> class.
        /// </summary>
        public CreateHistoricalContextHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetHistoricalContextRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<HistoricalContextProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        /// <summary>
        /// Created result should not be null.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task CreatedHistoricalContextShouldNotBeNull()
        {
            // Arrange

            var handler = new CreateHistoricalContextHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new CreateHistoricalContextCommand(new BLL.Dto.Timeline.HistoricalContextDto() { Title = "TEST" }), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }

        /// <summary>
        /// Created result title should be "TEST".
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task CreatedHistoricalContextTitleShouldBeTest()
        {
            // Arrange

            var handler = new CreateHistoricalContextHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new CreateHistoricalContextCommand(new BLL.Dto.Timeline.HistoricalContextDto() { Title = "TEST" }), CancellationToken.None);

            // Assert
            result.Value.Title.Should().Equals("TEST");
        }
    }
}
