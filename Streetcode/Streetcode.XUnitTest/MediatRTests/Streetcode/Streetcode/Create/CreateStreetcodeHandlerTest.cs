using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.Streetcode;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Streetcode;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.Create;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Streetcode.Streetcode.Create
{
    public class CreateStreetcodeHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public CreateStreetcodeHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetStreetcodeRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<StreetcodeProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        [Fact]
        public async Task CreateStreetcode_DtoIsNull_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new CreateStreetcodeHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            BaseStreetcodeDto? streetcodeDto = null;
            var request = new CreateStreetcodeCommand(streetcodeDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }
    }
}
