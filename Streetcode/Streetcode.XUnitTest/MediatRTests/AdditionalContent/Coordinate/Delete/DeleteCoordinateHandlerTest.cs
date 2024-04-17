namespace Streetcode.XUnitTest.MediatRTests.AdditionalContent.Coordinate.Delete
{
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Moq;
    using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Delete;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    public class DeleteCoordinateHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;

        public DeleteCoordinateHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetCoordinateRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<BLL.Mapping.AdditionalContent.Coordinates.StreetcodeCoordinateProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handler_WrongId_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new DeleteCoordinateHandler(_mockRepository.Object, _mapper);

            int wrongId = 10;
            DeleteCoordinateCommand request = new DeleteCoordinateCommand(wrongId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_CorrectId_IsSuccessShouldBeTrue()
        {
            // Arrange
            var handler = new DeleteCoordinateHandler(_mockRepository.Object, _mapper);

            int correctId = 1;
            DeleteCoordinateCommand request = new DeleteCoordinateCommand(correctId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}