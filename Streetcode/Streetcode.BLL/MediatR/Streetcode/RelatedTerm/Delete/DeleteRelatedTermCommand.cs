// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent;

// Necessaru namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.RelatedTerm.Delete
{
    /// <summary>
    /// Query, that requests a handler to delete a related term by given word.
    /// </summary>
    /// <param name="Word">
    /// Related term Word to delete.
    /// </param>
    public record DeleteRelatedTermCommand(string Word)
        : IRequest<Result<RelatedTermDto>>;
}
