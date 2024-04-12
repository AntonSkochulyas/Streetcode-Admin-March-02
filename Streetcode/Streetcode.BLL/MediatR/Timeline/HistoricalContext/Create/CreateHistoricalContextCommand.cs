using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Timeline;

namespace Streetcode.BLL.MediatR.Timeline.HistoricalContext.Create
{
    public record CreateHistoricalContextCommand(HistoricalContextDto NewHistoricalContext)
        : IRequest<Result<HistoricalContextDto>>;
}
