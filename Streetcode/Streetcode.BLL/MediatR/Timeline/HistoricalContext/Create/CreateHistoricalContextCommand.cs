// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Timeline;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Timeline.HistoricalContext.Create
{
    /// <summary>
    /// Command, that requests a handler to create a new historical context.
    /// </summary>
    /// <param name="newHistoricalContext">
    /// New historical context.
    /// </param>
    public record CreateHistoricalContextCommand(HistoricalContextDto NewHistoricalContext)
        : IRequest<Result<HistoricalContextDto>>;
}
