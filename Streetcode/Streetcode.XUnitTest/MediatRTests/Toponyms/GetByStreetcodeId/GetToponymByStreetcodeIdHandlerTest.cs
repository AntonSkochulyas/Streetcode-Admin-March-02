using Ardalis.Specification;
using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.Toponyms;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Toponyms;
using Streetcode.BLL.MediatR.Toponyms.GetByStreetcodeId;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Entities.Toponyms;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Toponyms;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Toponyms.GetByStreetcodeId
{
    public class GetToponymByStreetcodeIdHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public GetToponymByStreetcodeIdHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetToponymsRepositoryMock();
            _mockLogger = new Mock<ILoggerService>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ToponymProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_NullRequest_LogsError()
        {
            // Arrange
            SetupRepository();
            _mockRepository.Setup(x => x.ToponymRepository.GetItemsBySpecAsync(It.IsAny<GetByStreetcodeIdToponymSpec>()))
                           .ReturnsAsync((IEnumerable<Toponym>)null);
            var handler = new GetToponymsByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetToponymsByStreetcodeIdQuery(1), CancellationToken.None);

            // Assert
            _mockLogger.Verify(x => x.LogError(It.IsAny<GetToponymsByStreetcodeIdQuery>(), It.IsAny<string>()), Times.Once);
            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ShouldReturnNotNullOrEmpty()
        {
            // Arrange
            SetupRepository();
            var handler = new GetToponymsByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetToponymsByStreetcodeIdQuery(1), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Value.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task ShouldReturnTypeOfToponymDto()
        {
            // Arrange
            SetupRepository();
            var handler = new GetToponymsByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetToponymsByStreetcodeIdQuery(1), CancellationToken.None);

            // Assert
            result.Value.ToList().Should().BeOfType<List<ToponymDto>>();
        }

        [Fact]
        public async Task CountShouldBe4()
        {
            // Arrange
            SetupRepository();
            var handler = new GetToponymsByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetToponymsByStreetcodeIdQuery(1), CancellationToken.None);

            // Assert
            result.Value.Count().Should().Be(4);
        }

        [Fact]
        public async Task Handle_ReturnsEmptyListWhenNoToponymsFound()
        {
            // Arrange
            SetupRepository();
            _mockRepository.Setup(x => x.ToponymRepository.GetItemsBySpecAsync(It.IsAny<GetByStreetcodeIdToponymSpec>()))
                           .ReturnsAsync(new List<Toponym>());

            var handler = new GetToponymsByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetToponymsByStreetcodeIdQuery(5), CancellationToken.None);

            // Assert
            result.Value.Should().BeEmpty();
        }

        [Fact]
        public async Task Handle_ReturnsDistinctToponyms()
        {
            // Arrange
            SetupRepository();
            var handler = new GetToponymsByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetToponymsByStreetcodeIdQuery(1), CancellationToken.None);

            // Assert
            result.Value.Should().OnlyHaveUniqueItems();
        }

        private void SetupRepository()
        {
            var streetcodeContents = new List<StreetcodeContent>()
            {
               new StreetcodeContent()
               {
                   Id = 1,
               },
               new StreetcodeContent()
               {
                   Id = 2,
               },
               new StreetcodeContent()
               {
                   Id = 3,
               },
            };

            var toponyms = new List<Toponym>()
            {
                new Toponym()
                {
                    Id = 1,
                    Community = "First community",
                    AdminRegionNew = "First region new",
                    AdminRegionOld = "First region old",
                    Oblast = "First",
                    StreetName = "First streetname",
                    Streetcodes = streetcodeContents
                },
                new Toponym()
                {
                    Id = 2,
                    Community = "Second community",
                    AdminRegionNew = "Second region new",
                    AdminRegionOld = "Second region old",
                    Oblast = "Second",
                    StreetName = "Second streetname",
                    Streetcodes = streetcodeContents
                },
                new Toponym()
                {
                    Id = 3,
                    Community = "Third community",
                    AdminRegionNew = "Third region new",
                    AdminRegionOld = "Third region old",
                    Oblast = "Third",
                    StreetName = "Third streetname",
                    Streetcodes = streetcodeContents
                },
                new Toponym()
                {
                    Id = 4,
                    Community = "Fourth community",
                    AdminRegionNew = "Fourth region new",
                    AdminRegionOld = "Fourth region old",
                    Oblast = "Fourth",
                    StreetName = "Fourth streetname",
                    Streetcodes = streetcodeContents
                },
            };

            _mockRepository.Setup(repo => repo.ToponymRepository.GetItemsBySpecAsync(
            It.IsAny<ISpecification<Toponym>>()))
            .ReturnsAsync((GetByStreetcodeIdToponymSpec spec) =>
            {
                int streetcodeId = spec.StreetcodeId;
                return toponyms.Where(x => x.Streetcodes.Any(s => s.Id == streetcodeId));
            });
        }
    }
}
