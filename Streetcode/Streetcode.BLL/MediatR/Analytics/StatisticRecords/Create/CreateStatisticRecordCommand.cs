// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Analytics;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Analytics.StatisticRecords.Create
{
    /// <summary>
    /// Command, that request handler to create a new statistic record.
    /// </summary>
    /// <param name="StatisticRecordDto">
    /// Statistic record to add in database.
    /// </param>
    public record CreateStatisticRecordCommand(CreateStatisticRecordDto CreateStatisticRecordDto)
        : IRequest<Result<StatisticRecordDto>>;
}
