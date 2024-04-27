using AutoMapper;
using FluentAssertions;
using Moq;
using Streetcode.BLL.Dto.Analytics;
using Streetcode.BLL.Mapping.Analytics;
using Streetcode.BLL.MediatR.Analytics.StatisticRecords.Create;
using Streetcode.BLL.MediatR.Analytics.StatisticRecords.Delete;
using Streetcode.DAL.Entities.Analytics;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.XUnitTest.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Streetcode.XUnitTest.MediatRTests.Analytics.Delete
{
    public class DeleteStatisticRecordHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRepositoryWrapper> _mockRepository;

        public DeleteStatisticRecordHandlerTest()
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
        public async Task DeleteStatisticRecord_StatisticRecordIdIsNotValid_IsFailedShouldBeTrue()
        {
            // Arrange
            var handler = new DeleteStatisticRecordHandler(_mapper, _mockRepository.Object);
            var invalidId = 10;
            var request = new DeleteStatisticRecordCommand(invalidId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsFailed.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteStatisticRecord_StatisticRecordIdIsValid_DeleteShouldBeCalled()
        {
            // Arrange
            var handler = new DeleteStatisticRecordHandler(_mapper, _mockRepository.Object);
            var correctId = 1;
            var request = new DeleteStatisticRecordCommand(correctId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            _mockRepository.Verify(
                x => x.StatisticRecordRepository
                .Delete(It.IsAny<StatisticRecord>()), Times.Once);
        }

        [Fact]
        public async Task DeleteStatisticRecord_StatisticRecordIdIsValid_IsSuccessShouldBeTrue()
        {
            // Arrange
            var handler = new DeleteStatisticRecordHandler(_mapper, _mockRepository.Object);
            var correctId = 1;
            var request = new DeleteStatisticRecordCommand(correctId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteStatisticRecord_StatisticRecordIdIsValid_ResultShouldBeOfTypeStatisticRecordDto()
        {
            // Arrange
            var handler = new DeleteStatisticRecordHandler(_mapper, _mockRepository.Object);
            var correctId = 1;
            var request = new DeleteStatisticRecordCommand(correctId);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Value.Should().BeOfType<StatisticRecordDto>();
        }
    }
}
