using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Mapping.Media.Images;
using Streetcode.BLL.MediatR.Media.ImageMain.GetAll;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.GetAll;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Media.ImageMain.GetAll
{
    public class GetAllImageMainHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IBlobService> _blobService;

        public GetAllImageMainHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetImageMainRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ImageMainProfile>();

            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();

            _blobService = new Mock<IBlobService>();
        }

        [Fact]
        public async Task GetAll_RepositoryIsEmpty_IsFailedShouldBeTrue()
        {
            // Arrange
            _mockRepository.Setup(x => x.ImageMainRepository.GetAllAsync(It.IsAny<Expression<Func<DAL.Entities.Media.Images.ImageMain, bool>>>(),
                It.IsAny<Func<IQueryable<DAL.Entities.Media.Images.ImageMain>,
                IIncludableQueryable<DAL.Entities.Media.Images.ImageMain, object>>>()))
                .Returns(Task.FromResult<IEnumerable<DAL.Entities.Media.Images.ImageMain>?>(null));
            var handler = new GetAllImagesMainHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetAllImagesMainQuery(), CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task GetAll_RepositoryIsNotEmpty_IsSuccessShouldBeTrue()
        {
            // Arrange
            var handler = new GetAllImagesMainHandler(_mockRepository.Object, _mapper, _blobService.Object, _mockLogger.Object);

            // Act
            var result = await handler.Handle(new GetAllImagesMainQuery(), CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
