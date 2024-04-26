namespace Streetcode.XUnitTest.MediatRTests.Team.TeamMemberLinks;

using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.Team;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Team;
using Streetcode.BLL.MediatR.Team.TeamMembersLinks.GetAll;
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
        _mockRepository = RepositoryMocker.GetTeamLinksRepositoryMock();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<TeamLinkProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

        _mockLogger = new Mock<ILoggerService>();
    }

    [Fact]
    public async Task ShouldReturnNotNullOrEmpty()
    {
        // Arrange
        var handler = new GetAllTeamLinkHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetAllTeamLinkQuery(), CancellationToken.None);

        // Assert
        result.Value.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldReturnTypeOfPositionDTO()
    {
        // Arrange
        var handler = new GetAllTeamLinkHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetAllTeamLinkQuery(), CancellationToken.None);

        // Assert
        result.Value.Should().BeOfType<List<TeamMemberLinkDto>>();
    }

    [Fact]
    public async Task CountShouldBeFour()
    {
        // Arrange
        var handler = new GetAllTeamLinkHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetAllTeamLinkQuery(), CancellationToken.None);

        // Assert
        result.Value.Count().Should().Be(4);
    }
}