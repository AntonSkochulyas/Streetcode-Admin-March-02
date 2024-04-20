// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.RelatedFigure;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.RelatedFigure.Delete
{
    /// <summary>
    /// Query, that requests a handler to delete a related figure by given id of observer and target.
    /// </summary>
    /// <param name="ObserverId">
    /// <param name="TargetId">
    /// Partner id to delete.
    /// </param>
    public record DeleteRelatedFigureCommand(int ObserverId, int TargetId)
        : IRequest<Result<RelatedFigureDto>>;
}