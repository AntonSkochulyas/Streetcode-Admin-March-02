using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.Media.Images;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Sources;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.GetAll;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.GetAllCategoryContentsByStreetcodeIdAndSourceLinkCategoryId;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.GetCategoryContentsByStreetcodeIdAndSourceLinkCategoryId;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Sources.SourceLinkCategory.GetAll
{
    public class GetAllSourceLinkCategoriesHandlerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly IMapper _mapper;
        private readonly Mock<IBlobService> _mockBlob;
        private readonly Mock<ILoggerService> _mockLogger;

        public GetAllSourceLinkCategoriesHandlerTest()
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
        public async Task GetAllNotNullTest()
        {
            // Arrange
            var handler = new GetAllCategoriesHandler(_mockRepository.Object, _mapper, _mockBlob.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetAllCategoriesQuery(), CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAllSecondBlobNameShouldBeSpecific()
        {
            // Arrange
            var handler = new GetAllCategoriesHandler(_mockRepository.Object, _mapper, _mockBlob.Object, _mockLogger.Object);
            string expected = "Second title";

            // Act
            var result = await handler.Handle(new GetAllCategoriesQuery(), CancellationToken.None);

            // Assert
            result.Value.ElementAt(1).Title.Should().Be(expected);
        }
    }
}
