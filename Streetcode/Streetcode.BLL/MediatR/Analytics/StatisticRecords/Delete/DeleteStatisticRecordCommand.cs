// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Analytics;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Analytics.StatisticRecords.Delete
{
    /// <summary>
    /// Command, that request handler to delete a statistic record with given id.
    /// </summary>
    /// <param name="Id">
    /// Statistic record id to delete.
    /// </param>
    public record DeleteStatisticRecordCommand(int Id)
        : IRequest<Result<StatisticRecordDto>>;
}
