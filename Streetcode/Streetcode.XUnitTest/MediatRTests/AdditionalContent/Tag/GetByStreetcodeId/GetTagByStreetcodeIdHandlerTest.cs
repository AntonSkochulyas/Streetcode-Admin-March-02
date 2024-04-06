namespace Streetcode.XUnitTest.MediatRTests.AdditionalContent.Tag.GetByStreetcodeId
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using AutoMapper;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore.Query;
    using Moq;
    using Streetcode.BLL.Dto.AdditionalContent.Tag;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.AdditionalContent;
    using Streetcode.BLL.MediatR.AdditionalContent.Tag.GetByStreetcodeId;
    using Streetcode.DAL.Entities.AdditionalContent;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Streetcode.XUnitTest.Mocks;
    using Xunit;

    public class GetTagByStreetcodeIdHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        public GetTagByStreetcodeIdHandlerTest()
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
        public async Task Handler_GetByStreetcodeValidId_ResultShouldNotBeNull()
        {
            // Arrange
            var handler = new GetTagByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            int validId = 1;
            var request = new GetTagByStreetcodeIdQuery(validId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task Handler_GetByStreetcodeValidId_ResultShouldBeOfTypeIEnumerableStreetcodeTagDto()
        {
            // Arrange
            var handler = new GetTagByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            int validId = 1;
            var request = new GetTagByStreetcodeIdQuery(validId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().BeOfType<List<StreetcodeTagDto>>();
        }

        [Fact]
        public async Task Handler_GetByStreetcodeInvalidId_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new GetTagByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            int invalidId = 10;
            var request = new GetTagByStreetcodeIdQuery(invalidId);

            _mockRepository.Setup(x => x.StreetcodeTagIndexRepository.GetAllAsync(
                It.IsAny<Expression<Func<StreetcodeTagIndex, bool>>>(),
                It.IsAny<Func<IQueryable<StreetcodeTagIndex>, IIncludableQueryable<StreetcodeTagIndex, object>>>()))
            .Returns(Task.FromResult<IEnumerable<StreetcodeTagIndex>>(null));

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }
    }
}
