using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Analytics;

namespace Streetcode.BLL.MediatR.Analytics.StatisticRecords.Create
{
    public record CreateStatisticRecordCommand(StatisticRecordDto StatisticRecordDto) : IRequest<Result<StatisticRecordDto>>;
}
