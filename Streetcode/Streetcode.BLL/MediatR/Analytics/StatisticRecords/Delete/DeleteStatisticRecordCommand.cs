using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Analytics;

namespace Streetcode.BLL.MediatR.Analytics.StatisticRecords.Delete
{
    public record DeleteStatisticRecordCommand(int Id)
        : IRequest<Result<StatisticRecordDto>>;
}
