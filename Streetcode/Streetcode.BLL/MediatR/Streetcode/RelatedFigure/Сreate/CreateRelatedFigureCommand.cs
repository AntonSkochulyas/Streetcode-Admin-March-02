// Necessary usings.
using FluentResults;
using MediatR;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.RelatedFigure.Create
{
    /// <summary>
    /// Command, that requests a handler to create a new related figure.
    /// </summary>
    /// <param name="newRelatedFigure">
    /// New related figure.
    /// </param>
    public record CreateRelatedFigureCommand(int ObserverId, int TargetId)
        : IRequest<Result<Unit>>;
}
