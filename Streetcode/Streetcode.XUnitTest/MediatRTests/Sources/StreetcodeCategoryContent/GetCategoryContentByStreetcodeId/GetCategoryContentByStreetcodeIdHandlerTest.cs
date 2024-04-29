using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Sources;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.GetCategoryContentByStreetcodeId;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Sources.StreetcodeCategoryContent.GetCategoryContentByStreetcodeId
{
    public class GetCategoryContentByStreetcodeIdHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public GetCategoryContentByStreetcodeIdHandlerTest()
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
        public async Task GetCategoryContentByStreetcodeId_WrongStreetcodeId_IsFailedShouldBeTrue()
        {
            // Arrange
            int wrongStreetcodeId = 100;
            int correctSourceLinkId = 1;
            var handler = new GetCategoryContentByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            var request = new GetCategoryContentByStreetcodeIdQuery(wrongStreetcodeId, correctSourceLinkId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task GetCategoryContentByStreetcodeId_WrongSourceLinkId_IsFailedShouldBeTrue()
        {
            // Arrange
            int correctStreetcodeId = 1;
            int wrongSourceLinkId = 100;
            var handler = new GetCategoryContentByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            var request = new GetCategoryContentByStreetcodeIdQuery(correctStreetcodeId, wrongSourceLinkId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task GetCategoryContentByStreetcodeId_CorrectId_IsSuccessShouldBeTrue()
        {
            // Arrange
            int correctStreetcodeId = 1;
            int correctSourceLinkId = 1;
            var handler = new GetCategoryContentByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            var request = new GetCategoryContentByStreetcodeIdQuery(correctStreetcodeId, correctSourceLinkId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
