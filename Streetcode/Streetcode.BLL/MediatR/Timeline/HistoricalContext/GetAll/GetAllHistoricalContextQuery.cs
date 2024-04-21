// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Timeline;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Timeline.HistoricalContext.GetAll
{
    /// <summary>
    /// Query, that requests a handler to get all historicalcontexts from database.
    /// </summary>
    public record GetAllHistoricalContextQuery : IRequest<Result<IEnumerable<HistoricalContextDto>>>
    {
        public GetAllHistoricalContextQuery()
        {
        }
    }
}
