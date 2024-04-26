// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent.Fact;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.Fact.Delete
{
    /// <summary>
    /// Query, that requests a handler to delete a fact by given id.
    /// </summary>
    /// <param name="id">
    /// Fact id to delete.
    /// </param>
    public record DeleteFactCommand(int Id)
        : IRequest<Result<FactDto>>;
}