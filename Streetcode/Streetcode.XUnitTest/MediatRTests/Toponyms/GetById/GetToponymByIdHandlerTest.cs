using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Toponyms;
using Streetcode.BLL.MediatR.Toponyms.GetById;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.StreetcodeTests.Toponym;
public class GetByIdHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<IRepositoryWrapper> _mockRepository;
    private readonly Mock<ILoggerService> _mockLogger;

    public GetByIdHandlerTest()
    {
        _mockRepository = RepositoryMocker.GetToponymsRepositoryMock();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<ToponymProfile>();
        });

        _mapper = mapperConfig.CreateMapper();

        _mockLogger = new Mock<ILoggerService>();
    }

    [Fact]
    public async Task WithExistingId_ShouldReturnToponymDto()
    {
        // Arrange
        var handler = new GetToponymByIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetToponymByIdQuery(3), CancellationToken.None);

        // Assert
        result.Value.Should().NotBeNull();
        result.Value.StreetName.Should().Be("First streetname"); // Перевірка, чи повертається правильний ідентифікатор
    }

    [Fact]
    public async Task WithNotExistingId10_ShouldReturnNull()
    {
        // Arrange
        var handler = new GetToponymByIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

        // Act
        var result = await handler.Handle(new GetToponymByIdQuery(10), CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
    }
}
