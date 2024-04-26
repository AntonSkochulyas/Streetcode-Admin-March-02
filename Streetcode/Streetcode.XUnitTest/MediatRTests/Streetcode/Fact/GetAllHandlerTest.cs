namespace Streetcode.XUnitTest.MediatRTests.StreetcodeTests.Fact;

using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.Streetcode.TextContent.Fact;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Streetcode.TextContent;
using Streetcode.BLL.MediatR.Streetcode.Fact.GetAll;
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
        var handler = new GetAllFactsHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetAllFactsQuery(), CancellationToken.None);

        // Assert
        result.Value.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ShouldReturnTypeOfFactDto()
    {
        // Arrange
        var handler = new GetAllFactsHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetAllFactsQuery(), CancellationToken.None);

        // Assert
        result.Value.Should().BeOfType<List<FactDto>>();
    }

    [Fact]
    public async Task CountShouldBe9()
    {
        // Arrange
        var handler = new GetAllFactsHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetAllFactsQuery(), CancellationToken.None);

        // Assert
        result.Value.Count().Should().Be(9);
    }
}