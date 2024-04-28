namespace Streetcode.XUnitTest.MediatRTests.Transactions.GetAll
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.News;
    using Streetcode.BLL.Dto.Transactions;
    using Streetcode.BLL.Interfaces.BlobStorage;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.Transactions;
    using Streetcode.BLL.MediatR.Transactions.TransactionLink.GetAll;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    public class GetAllTransactLinksHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public GetAllTransactLinksHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetTransactionsRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<TransactionLinkProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        [Fact]
        public async Task Handler_GetAll_ResultShouldNotBeNullOrEmpty()
        {
            // Arrange
            var handler = new GetAllTransactLinksHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            var request = new GetAllTransactLinksQuery();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_GetAll_ResultShouldBeOfTypeSubtitleDTO()
        {
            // Arrange
            var handler = new GetAllTransactLinksHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            var request = new GetAllTransactLinksQuery();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().BeOfType<List<TransactLinkDto>>();
        }

        [Fact]
        public async Task Handler_GetAll_CountShouldBeFour()
        {
            // Arrange
            var handler = new GetAllTransactLinksHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            var request = new GetAllTransactLinksQuery();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Count().Should().Be(4);

        }

        [Fact]
        public async Task Handler_GetAll_Count_Should_NotBe_Five()
        {
            // Arrange
            var handler = new GetAllTransactLinksHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            var request = new GetAllTransactLinksQuery();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Count().Should().NotBe(5);
        }


    }
}
