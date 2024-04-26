namespace Streetcode.XUnitTest.MediatRTests.Team;

using Ardalis.Specification;
using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.Team;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Team;
using Streetcode.BLL.MediatR.Team.GetAll;
using Streetcode.DAL.Entities.Team;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Team;
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
        SetupRepository();
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
        SetupRepository();
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
        SetupRepository();
        var handler = new GetAllMainTeamHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetAllMainTeamQuery(), CancellationToken.None);

        // Assert
        result.Value.Count().Should().Be(2);
    }

    private void SetupRepository()
    {
        var members = new List<TeamMember>()
        {
            new TeamMember { Id = 1, FirstName = "John", LastName = "Doe", Description = "description1", IsMain = true, ImageId = 1 },
            new TeamMember { Id = 2, FirstName = "Jane", LastName = "Mur", Description = "description2", IsMain = true, ImageId = 2 },
            new TeamMember { Id = 3, FirstName = "Mila", LastName = "Lyubow", Description = "description3", IsMain = false, ImageId = 3 },
            new TeamMember { Id = 4, FirstName = "Orest", LastName = "Fifa", Description = "description4", IsMain = false, ImageId = 2 },
            new TeamMember { Id = 5, FirstName = "Alex", LastName = "Smith", Description = "description5", IsMain = false, ImageId = 4 },
            new TeamMember { Id = 6, FirstName = "Emily", LastName = "Johnson", Description = "description6", IsMain = false, ImageId = 5 }
        };

        _mockRepository.Setup(repo => repo.TeamRepository.GetItemsBySpecAsync(
        It.IsAny<ISpecification<TeamMember>>()))
        .ReturnsAsync((GetAllMainTeamSpec spec) =>
        {
            return members.Where(t => t.IsMain);
        });
    }
}