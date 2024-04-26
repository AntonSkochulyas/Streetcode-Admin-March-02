namespace Streetcode.XUnitTest.MediatRTests.Toponyms.GetById
{
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Toponyms;
    using Streetcode.BLL.MediatR.Toponyms.GetById;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class GetToponymByIdHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetToponymByIdHandlerTest"/> class.
        /// </summary>
        public GetToponymByIdHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetToponymsRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ToponymProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        /// <summary>
        /// Get by id not null test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetByIdNotNullTest()
        {
            // Arrange
            var handler = new GetToponymByIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetToponymByIdQuery(1), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }

        /// <summary>
        /// Get by id first item should be first item.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetByIdFirstShouldBeFirstTest()
        {
            // Arrange
            var handler = new GetToponymByIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetToponymByIdQuery(1), CancellationToken.None);

            // Assert
            result.Value.StreetName.Should().Be("First streetname");
        }

        /// <summary>
        /// Get by id second item streetname should not be fourth item description test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetByIdSecondShouldNotBeFourthTest()
        {
            // Arrange
            var handler = new GetToponymByIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetToponymByIdQuery(2), CancellationToken.None);

            // Assert
            result.Value.StreetName.Should().NotBe("Fourth streetname");
        }

        [Fact]
        public async Task WithNotExistingId10_ShouldReturnNull()
        {
            // Arrange
            var handler = new GetToponymByIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetToponymByIdQuery(10), CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }
    }
}