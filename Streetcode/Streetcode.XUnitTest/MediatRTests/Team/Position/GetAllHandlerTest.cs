namespace Streetcode.XUnitTest.MediatRTests.Team.Position;

using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.Team;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Team;
using Streetcode.BLL.MediatR.Team.GetAll;
using Streetcode.BLL.MediatR.Team.Position.GetAll;
using Streetcode.DAL.Entities.Team;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;

public class GetAllHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IRepositoryWrapper> _mockRepository;
    private readonly Mock<ILoggerService> _mockLogger;

    public GetAllHandlerTest()
    {
        _mockRepository = RepositoryMocker.GetTeamPositionRepositoryMock();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<PositionProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

        _mockLogger = new Mock<ILoggerService>();
    }

    [Fact]
    public async Task ShouldReturnNotNullOrEmpty()
    {
        // Arrange
        var handler = new GetAllPositionsHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetAllPositionsQuery(), CancellationToken.None);

        // Assert
        result.Value.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldReturnTypeOfPositionDTO()
    {
        // Arrange
        var handler = new GetAllPositionsHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetAllPositionsQuery(), CancellationToken.None);

        // Assert
        result.Value.Should().BeOfType<List<PositionDto>>();
    }

    [Fact]
    public async Task CountShouldBeThree()
    {
        // Arrange
        var handler = new GetAllPositionsHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetAllPositionsQuery(), CancellationToken.None);

        // Assert
        result.Value.Count().Should().Be(3);
    }
}