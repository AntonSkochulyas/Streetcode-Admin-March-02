namespace Streetcode.XUnitTest.MediatRTests.Transactions.GetByStreetcodeId
{
    using System.Threading.Tasks;
    using Ardalis.Specification;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Transactions;
    using Streetcode.BLL.MediatR.Transactions.TransactionLink.GetByStreetcodeId;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Entities.Toponyms;
    using Streetcode.DAL.Entities.Transactions;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.DAL.Specification.Toponyms;
    using Streetcode.DAL.Specification.Transactions.TransactionLink;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    /// <summary>
    /// Tested successfully.
    /// </summary>
    public class GetByStreetcodeIdTransactLinksHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetByStreetcodeIdTransactLinksHandlerTest"/> class.
        /// </summary>
        public GetByStreetcodeIdTransactLinksHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetTransactionsRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<TransactionLinkProfile>();
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
            SetupRepository();
            var handler = new GetTransactLinkByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetTransactLinkByStreetcodeIdQuery(1), CancellationToken.None);

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
            SetupRepository();
            var handler = new GetTransactLinkByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetTransactLinkByStreetcodeIdQuery(1), CancellationToken.None);

            // Assert
            result.Value.Url.Should().Be("1Url");
        }

        /// <summary>
        /// Get by id second item url should not be fourth item description test.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task GetByIdSecondShouldNotBeFourthTest()
        {
            // Arrange
            SetupRepository();
            var handler = new GetTransactLinkByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetTransactLinkByStreetcodeIdQuery(2), CancellationToken.None);

            // Assert
            result.Value.Url.Should().NotBe("4Url");
        }

        [Fact]
        public async Task WithNotExistingId10_ShouldReturnNull()
        {
            // Arrange
            SetupRepository();
            var handler = new GetTransactLinkByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetTransactLinkByStreetcodeIdQuery(10), CancellationToken.None);

            // Assert
            result.Value.Should().BeNull();
        }

        private void SetupRepository()
        {
            var transactions = new List<TransactionLink>
            {
                new TransactionLink() { Id = 1, Url = "1Url", UrlTitle = "1UrlTitle", StreetcodeId = 1, Streetcode = new DAL.Entities.Streetcode.StreetcodeContent() },
                new TransactionLink() { Id = 2, Url = "2Url", UrlTitle = "2UrlTitle", StreetcodeId = 2, Streetcode = new DAL.Entities.Streetcode.StreetcodeContent() },
                new TransactionLink() { Id = 3, Url = "3Url", UrlTitle = "3UrlTitle", StreetcodeId = 3, Streetcode = new DAL.Entities.Streetcode.StreetcodeContent() },
                new TransactionLink() { Id = 4, Url = "4Url", UrlTitle = "4UrlTitle", StreetcodeId = 4, Streetcode = new DAL.Entities.Streetcode.StreetcodeContent() },
            };
            _mockRepository.Setup(repo => repo.TransactLinksRepository.GetItemBySpecAsync(
        It.IsAny<ISpecification<TransactionLink>>()))
        .ReturnsAsync((GetByStreetcodeIdTransactionLinkSpec spec) =>
        {
            int streetcodeId = spec.StreetcodeId;

            var transactlinks = transactions.FirstOrDefault(s => s.StreetcodeId == streetcodeId);

            if (transactlinks != null)
            {
                // Доступ до властивостей transactlinks
                return transactlinks;
            }
            else
            {
                // Обробка випадку, коли transactlinks == null
                return null; // Або відповідний об'єкт Result
            }
        });
        }
    }
}