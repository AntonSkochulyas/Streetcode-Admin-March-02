using System.Linq.Expressions;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Streetcode.BLL.Mapping.Analytics;
using Streetcode.BLL.MediatR.Analytics.StatisticRecords.GetAll;
using Streetcode.DAL.Entities.Analytics;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Analytics.GetAll
{
    public class GetAllStatisticRecordHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;

        public GetAllStatisticRecordHandlerTest()
        {
            _mockRepository = RepositoryMocker.GetStatisticRecordRepositoryMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<CreateStatisticRecordProfile>();
                c.AddProfile<StatisticRecordProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetAllStatisticRecord_ValidData_IsSuccessShouldBeTrue()
        {
            // Arrange
            var handler = new GetAllStatisticRecordsHandler(_mapper, _mockRepository.Object);
            var request = new GetAllStatisticRecordsQuery();

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
