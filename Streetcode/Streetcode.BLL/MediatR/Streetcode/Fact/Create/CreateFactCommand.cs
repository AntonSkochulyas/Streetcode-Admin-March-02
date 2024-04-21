// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent.Fact;

// Necessaru namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.Fact.Create
{
    /// <summary>
    /// Command, that requests a handler to create a new fact.
    /// </summary>
    /// <param name="newFact">
    /// New fact.
    /// </param>
    public record CreateFactCommand(FactBaseDto Fact)
        : IRequest<Result<FactDto>>;
}