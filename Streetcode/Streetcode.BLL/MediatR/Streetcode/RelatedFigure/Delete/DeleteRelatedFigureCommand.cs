using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.RelatedFigure;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedFigure.Delete;

public record DeleteRelatedFigureCommand(int ObserverId, int TargetId)
    : IRequest<Result<RelatedFigureDto>>;
