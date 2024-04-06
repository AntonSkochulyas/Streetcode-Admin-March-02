namespace Streetcode.XUnitTest.MediatRTests.Team;

using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.Team;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Team;
using Streetcode.BLL.MediatR.Team.GetAll;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;

public class GetAllMainHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IRepositoryWrapper> _mockRepository;
    private readonly Mock<ILoggerService> _mockLogger;

    public GetAllMainHandlerTest()
    {
        _mockRepository = RepositoryMocker.GetTeamRepositoryMock();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<TeamProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

        _mockLogger = new Mock<ILoggerService>();
    }

    [Fact]
    public async Task ShouldReturnNotNullOrEmpty()
    {
        // Arrange
        var handler = new GetAllMainTeamHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetAllMainTeamQuery(), CancellationToken.None);

        // Assert
        result.Value.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldReturnsTypeOfTeamMemberDTO()
    {
        // Arrange
        var handler = new GetAllMainTeamHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetAllMainTeamQuery(), CancellationToken.None);

        // Assert
        result.Value.Should().BeOfType<List<TeamMemberDto>>();
    }

    [Fact]
    public async Task CountShouldBe2()
    {
        // Arrange
        var handler = new GetAllMainTeamHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetAllMainTeamQuery(), CancellationToken.None);

        // Assert
        result.Value.Count().Should().Be(2);
    }
}