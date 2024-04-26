// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent.Fact;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.Fact.GetAll
{
    /// <summary>
    /// Query, that requests a handler to get all facts from database.
    /// </summary>
    public record GetAllFactsQuery : IRequest<Result<IEnumerable<FactDto>>>
    {
        public GetAllFactsQuery()
        {
        }
    }
}