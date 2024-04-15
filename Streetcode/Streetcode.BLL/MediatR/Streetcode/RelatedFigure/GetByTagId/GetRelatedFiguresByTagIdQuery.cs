using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.RelatedFigure;

namespace Streetcode.BLL.MediatR.Streetcode.RelatedFigure.GetByTagId
{
  public record GetRelatedFiguresByTagIdQuery(int TagId)
        : IRequest<Result<IEnumerable<RelatedFigureDto>>>;
}
