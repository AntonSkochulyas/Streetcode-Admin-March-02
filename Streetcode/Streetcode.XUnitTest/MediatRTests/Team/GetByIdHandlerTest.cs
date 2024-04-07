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
        var handler = new GetByIdTeamHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetByIdTeamQuery(7), CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
    }
}