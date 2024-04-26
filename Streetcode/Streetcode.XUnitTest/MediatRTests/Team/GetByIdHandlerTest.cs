namespace Streetcode.XUnitTest.MediatRTests.Team;

using AutoMapper;
using Moq;
using Streetcode.BLL.Interfaces.Logging;
using FluentAssertions;
using Streetcode.BLL.Mapping.Team;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;
using Streetcode.BLL.MediatR.Team.GetById;
using Streetcode.DAL.Entities.Team;
using Streetcode.DAL.Specification.Sources.SourceLinkCategory;
using Ardalis.Specification;
using Streetcode.DAL.Specification.Team;

public class GetByIdHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IRepositoryWrapper> _mockRepository;
    private readonly Mock<ILoggerService> _mockLogger;

    public GetByIdHandlerTest()
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
    public async Task WithExistingId2_ShouldReturnDtoWithNameJane()
    {
        // Arrange
        SetupRepository();
        var handler = new GetByIdTeamHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetByIdTeamQuery(2), CancellationToken.None);

        // Assert
        result.Value.Should().NotBeNull();
        result.Value.FirstName.Should().BeEquivalentTo("Jane");
    }

    [Fact]
    public async Task WithNotExistingId7_ShouldReturnNull()
    {
        // Arrange
        SetupRepository();
        var handler = new GetByIdTeamHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetByIdTeamQuery(7), CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
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

        _mockRepository.Setup(repo => repo.TeamRepository.GetItemBySpecAsync(
        It.IsAny<ISpecification<TeamMember>>()))
        .ReturnsAsync((GetByIdTeamSpec spec) =>
        {
            int id = spec.Id;

            var member = members.FirstOrDefault(s => s.Id == id);

            return member;
        });
    }
}