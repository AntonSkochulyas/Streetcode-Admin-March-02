// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.Term.GetAll
{
    /// <summary>
    /// Query, that requests a handler to get all terms from database.
    /// </summary>
    public record GetAllTermsQuery : IRequest<Result<IEnumerable<TermDto>>>;
}
