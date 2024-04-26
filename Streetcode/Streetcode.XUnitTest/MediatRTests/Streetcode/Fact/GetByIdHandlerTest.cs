namespace Streetcode.XUnitTest.MediatRTests.StreetcodeTests.Fact;

using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Streetcode.TextContent;
using Streetcode.BLL.MediatR.Streetcode.Fact.GetById;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;

public class GetByIdHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IRepositoryWrapper> _mockRepository;
    private readonly Mock<ILoggerService> _mockLogger;

    public GetByIdHandlerTest()
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
    public async Task WithExistingId3_ShouldReturnDtoWithName3()
    {
        // Arrange
        var handler = new GetFactByIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetFactByIdQuery(3), CancellationToken.None);

        // Assert
        result.Value.Should().NotBeNull();
        result.Value.Title.Should().BeEquivalentTo("3");
    }

    [Fact]
    public async Task WithNotExistingId10_ShouldReturnNull()
    {
        // Arrange
        var handler = new GetFactByIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetFactByIdQuery(10), CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
    }
}