using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.News;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Media.Images;
using Streetcode.BLL.Mapping.Sources;
using Streetcode.BLL.MediatR.Newss.Create;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Create;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Sources.SourceLinkCategory.Create
{
    public class CreateSourceLinkCategoryHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public CreateSourceLinkCategoryHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetSourceRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<SourceLinkCategoryProfile>();
                c.AddProfile<SourceLinkSubCategoryProfile>();
                c.AddProfile<StreetcodeCategoryContentProfile>();
                c.AddProfile<ImageProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        [Fact]
        public async Task Handle_SourceLinkDtoIsNull_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new CreateSourceLinkCategoryHandler(_mapper, _mockRepository.Object, _mockLogger.Object);
            SourceLinkCategoryDto? sourceLinkDto = null;
            var request = new CreateSourceLinkCategoryCommand(sourceLinkDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_SourceLinkCategoryValidDto_IsSuccessShouldBeTrue()
        {
            // Arrange
            var handler = new CreateSourceLinkCategoryHandler(_mapper, _mockRepository.Object, _mockLogger.Object);
            SourceLinkCategoryDto? sourceLinkCategoryDto = new SourceLinkCategoryDto()
            {
                Id = 1,
                ImageId = 1,
                Title = "Test1"
            };
            var request = new CreateSourceLinkCategoryCommand(sourceLinkCategoryDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
