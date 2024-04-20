// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Create
{
    /// <summary>
    /// Command, that requests a handler to create a new related term.
    /// </summary>
    /// <param name="newRelatedTerm">
    /// New related term.
    /// </param>
    public record CreateRelatedTermCommand(RelatedTermDto RelatedTerm)
        : IRequest<Result<RelatedTermDto>>
    {
    }
}
