// Necessary usings
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Analytics;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.Analytics.StatisticRecords.GetAll
{
    /// <summary>
    /// Query, that requests handler to get all statistic records from database.
    /// </summary>
    public record GetAllStatisticRecordsQuery : IRequest<Result<IEnumerable<StatisticRecordDto>>>
    {
        public GetAllStatisticRecordsQuery()
        {
        }
    }
}
