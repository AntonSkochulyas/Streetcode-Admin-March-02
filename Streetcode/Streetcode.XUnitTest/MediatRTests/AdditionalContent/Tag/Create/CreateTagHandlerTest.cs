namespace Streetcode.XUnitTest.MediatRTests.AdditionalContent.Tag.Create
{
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.Dto.AdditionalContent;
    using Streetcode.BLL.Dto.AdditionalContent.Tag;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.AdditionalContent;
    using Streetcode.BLL.MediatR.AdditionalContent.Tag.Create;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    public class CreateTagHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public CreateTagHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetTagRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<TagProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        [Fact]
        public async Task Handler_ValidData_IsSuccessShouldBeTrue()
        {
            // Arrange
            var handler = new CreateTagHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            var tagDTO = new CreateTagDto() { Title = "Test" };
            var request = new CreateTagCommand(tagDTO);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_ValidData_ResultShouldBeOfTypeTagDTO()
        {
            // Arrange
            var handler = new CreateTagHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            var tagDTO = new CreateTagDto() { Title = "Test" };
            var request = new CreateTagCommand(tagDTO);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().BeOfType<TagDto>();
        }

        [Fact]
        public async Task Handler_CreateTag_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new CreateTagHandler(RepositoryMocker.GetTagRepositoryMockWithSettingException().Object, _mapper, _mockLogger.Object);
            var tagDTO = new CreateTagDto();
            var request = new CreateTagCommand(tagDTO);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }
    }
}
