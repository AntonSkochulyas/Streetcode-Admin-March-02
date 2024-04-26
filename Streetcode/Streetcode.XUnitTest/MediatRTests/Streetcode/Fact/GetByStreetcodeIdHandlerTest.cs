namespace Streetcode.XUnitTest.MediatRTests.StreetcodeTests.Fact;

using Ardalis.Specification;
using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.Streetcode.TextContent.Fact;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Streetcode.TextContent;
using Streetcode.BLL.MediatR.Streetcode.Fact.GetAll;
using Streetcode.BLL.MediatR.Streetcode.Fact.GetByStreetcodeId;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Streetcode.Fact;
using Streetcode.XUnitTest.Mocks;
using Xunit;

public class GetByStreetcodeIdHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IRepositoryWrapper> _mockRepository;
    private readonly Mock<ILoggerService> _mockLogger;

    public GetByStreetcodeIdHandlerTest()
    {
        _mockRepository = RepositoryMocker.GetFactRepositoryMock();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<FactProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

        _mockLogger = new Mock<ILoggerService>();
    }

    [Fact]
    public async Task ShouldReturnNotNullOrEmpty()
    {
        // Arrange
        SetupRepository();
        var handler = new GetFactByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetFactByStreetcodeIdQuery(1), CancellationToken.None);

        // Assert
        result.Value.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldReturnTypeOfFactDto()
    {
        // Arrange
        SetupRepository();
        var handler = new GetFactByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetFactByStreetcodeIdQuery(1), CancellationToken.None);

        // Assert
        result.Value.Should().BeOfType<List<FactDto>>();
    }

    [Fact]
    public async Task CountShouldBe4()
    {
        // Arrange
        SetupRepository();
        var handler = new GetFactByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetFactByStreetcodeIdQuery(1), CancellationToken.None);

        // Assert
        result.Value.Count().Should().Be(4);
    }

    private void SetupRepository()
    {
        _mockRepository.Setup(repo => repo.FactRepository.GetItemsBySpecAsync(
        It.IsAny<ISpecification<DAL.Entities.Streetcode.TextContent.Fact>>()))
        .ReturnsAsync((GetAllByStreetcodeIdFactSpec spec) =>
        {
            var facts = new List<DAL.Entities.Streetcode.TextContent.Fact>()
            {
                new DAL.Entities.Streetcode.TextContent.Fact { Id = 1, Title = "1", StreetcodeId = 1},
                new DAL.Entities.Streetcode.TextContent.Fact { Id = 2, Title = "2", StreetcodeId = 1},
                new DAL.Entities.Streetcode.TextContent.Fact { Id = 3, Title = "3", StreetcodeId = 1},
                new DAL.Entities.Streetcode.TextContent.Fact { Id = 4, Title = "4", StreetcodeId = 1},
                new DAL.Entities.Streetcode.TextContent.Fact { Id = 5, Title = "5", StreetcodeId = 2},
                new DAL.Entities.Streetcode.TextContent.Fact { Id = 6, Title = "6", StreetcodeId = 2},
                new DAL.Entities.Streetcode.TextContent.Fact { Id = 7, Title = "7", StreetcodeId = 2},
                new DAL.Entities.Streetcode.TextContent.Fact { Id = 8, Title = "8", StreetcodeId = 3},
                new DAL.Entities.Streetcode.TextContent.Fact { Id = 9, Title = "9", StreetcodeId = 3},
            };

            int streetcodeId = spec.StreetcodeId;

            var fact = facts.Where(s => s.StreetcodeId == streetcodeId);

            return fact;
        });
    }
}