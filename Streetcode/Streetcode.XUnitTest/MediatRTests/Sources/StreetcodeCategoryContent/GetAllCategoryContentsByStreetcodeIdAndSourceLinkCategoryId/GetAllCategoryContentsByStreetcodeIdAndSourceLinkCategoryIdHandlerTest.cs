using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.Media.Images;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Sources;
using Streetcode.BLL.MediatR.Sources.SourceLink.GetCategoriesByStreetcodeId;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.GetAllCategoryContentsByStreetcodeIdAndSourceLinkCategoryId;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.GetCategoryContentsByStreetcodeIdAndSourceLinkCategoryId;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.MediatRTests.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Sources.StreetcodeCategoryContent.GetAllCategoryContentsByStreetcodeIdAndSourceLinkCategoryId
{
    public class GetAllCategoryContentsByStreetcodeIdAndSourceLinkCategoryIdHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly IMapper _mapper;
        private readonly Mock<IBlobService> _mockBlob;
        private readonly Mock<ILoggerService> _mockLogger;

        public GetAllCategoryContentsByStreetcodeIdAndSourceLinkCategoryIdHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetSourceRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<SourceLinkCategoryProfile>();
                c.AddProfile<SourceLinkSubCategoryProfile>();
                c.AddProfile<StreetcodeCategoryContentProfile>();
                c.CreateMap<Image, ImageDto>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();

            _mockBlob = new Mock<IBlobService>();
        }

        [Fact]
        public async Task GetByIdNotNullTest()
        {
            // Arrange
            var handler = new GetAllCategoryContentsByStreetcodeIdAndSourceLinkCategoryIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetAllCategoryContentsByStreetcodeIdAndSourceLinkCategoryIdQuery(1, 1), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetByIdSecondBlobNameShouldBeSpecific()
        {
            // Arrange
            var handler = new GetAllCategoryContentsByStreetcodeIdAndSourceLinkCategoryIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            string expected = "Test2";

            // Act
            var result = await handler.Handle(new GetAllCategoryContentsByStreetcodeIdAndSourceLinkCategoryIdQuery(1, 1), CancellationToken.None);

            // Assert
            result.Value.ElementAt(1).Text.Should().Be(expected);
        }
    }
}
