namespace Streetcode.XUnitTest.MediatRTests.AdditionalContent.Coordinate.GetByStreetcoordinateId
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
    using Streetcode.BLL.Dto.AdditionalContent.Coordinates.Types;
    using Streetcode.BLL.Interfaces.Logging;
    using Streetcode.BLL.Mapping.AdditionalContent.Coordinates;
    using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.GetByStreetcodeId;
    using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;
    using Streetcode.DAL.Entities.Streetcode;
    using Streetcode.DAL.Repositories.Interfaces.Base;
    using Xunit;

    public class GetByStreetcoordinateIdHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;
        private readonly Mock<ILoggerService> _mockLogger;

        private readonly List<StreetcodeCoordinate> _coordinates = new List<StreetcodeCoordinate>
        {
            new StreetcodeCoordinate
            {
                Id = 1,
                StreetcodeId = 1,
            },
            new StreetcodeCoordinate
            {
                Id = 2,
                StreetcodeId = 1,
            },
        };

        public GetByStreetcoordinateIdHandlerTest()
        {
            _mockRepository = new Mock<IRepositoryWrapper>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<StreetcodeCoordinateProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _mockLogger = new Mock<ILoggerService>();
        }

        [Fact]
        public async Task Handle_ValidData_ValueShouldBeOfTypeListStreetcodeCoordinateDTO()
        {
            // Arrange
            await SetupRepository(_coordinates, new StreetcodeContent());

            var handler = new GetCoordinatesByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            int streetCodeid = 1;
            var getCoordinatesByStreetcodeIdQuery = new GetCoordinatesByStreetcodeIdQuery(streetCodeid);

            // Act
            var result = await handler.Handle(getCoordinatesByStreetcodeIdQuery, CancellationToken.None);

            // Assert
            result.Value.Should().BeOfType<List<StreetcodeCoordinateDto>>();
        }

        [Fact]
        public async Task Handle_StreetccodeCoordinatesIsEmpty_ResultShouldBeEmpty()
        {
            // Arrange
            await SetupRepository(new List<StreetcodeCoordinate>(), new StreetcodeContent());

            var handler = new GetCoordinatesByStreetcodeIdHandler(_mockRepository.Object, _mapper, _mockLogger.Object);
            int streetCodeid = 1;
            var getCoordinatesByStreetcodeIdQuery = new GetCoordinatesByStreetcodeIdQuery(streetCodeid);

            // Act
            var result = await handler.Handle(getCoordinatesByStreetcodeIdQuery, CancellationToken.None);

            // Assert
            result.Value.Should().BeEmpty();
        }

        private async Task SetupRepository(List<StreetcodeCoordinate> coordinates, StreetcodeContent streetcode)
        {
            _mockRepository.Setup(x => x.StreetcodeCoordinateRepository.GetAllAsync(
                It.IsAny<Expression<Func<StreetcodeCoordinate, bool>>>(),
                It.IsAny<Func<IQueryable<StreetcodeCoordinate>,
                IIncludableQueryable<StreetcodeCoordinate, object>>>())).ReturnsAsync(coordinates);

            _mockRepository.Setup(x => x.StreetcodeRepository
                .GetFirstOrDefaultAsync(
                   It.IsAny<Expression<Func<StreetcodeContent, bool>>>(),
                   It.IsAny<Func<IQueryable<StreetcodeContent>,
                    IIncludableQueryable<StreetcodeContent, object>>>()))
                .ReturnsAsync(streetcode);
        }
    }
}
