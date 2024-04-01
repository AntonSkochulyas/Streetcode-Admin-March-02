using AutoMapper;
using Moq;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Sources;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Create;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.MediatRTests.Mocks;
using Xunit;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Create;
using Streetcode.DAL.Entities.Sources;
using FluentAssertions;

namespace Streetcode.XUnitTest.MediatRTests.Sources.StreetcodeCategoryContent.Create
{
    public class CreateStreetcodeCategoryContentHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public CreateStreetcodeCategoryContentHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetSourceRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<SourceLinkCategoryProfile>();
                c.AddProfile<SourceLinkSubCategoryProfile>();
                c.AddProfile<StreetcodeCategoryContentProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        [Fact]
        public async Task Handle_StreetcodeCategoryContentDtoIsNull_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new CreateStreetcodeCategoryContentHandler(_mapper, _mockRepository.Object, _mockLogger.Object);
            StreetcodeCategoryContentDto? streetcodeCategoryContentDto = null;
            var request = new CreateStreetcodeCategoryContentCommand(streetcodeCategoryContentDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_SourceLinkCategoryValidDto_IsSuccessShouldBeTrue()
        {
            // Arrange
            var handler = new CreateStreetcodeCategoryContentHandler(_mapper, _mockRepository.Object, _mockLogger.Object);
            StreetcodeCategoryContentDto? streetcodeCategoryContentDto = new StreetcodeCategoryContentDto()
            {
                SourceLinkCategoryId = 1,
                StreetcodeId = 1,
                Text = "Test1"
            };
            var request = new CreateStreetcodeCategoryContentCommand(streetcodeCategoryContentDto);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
