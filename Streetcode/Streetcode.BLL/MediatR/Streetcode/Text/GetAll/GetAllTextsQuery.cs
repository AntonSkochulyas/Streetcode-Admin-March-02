// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent.Text;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.Text.GetAll
{
    /// <summary>
    /// Query, that requests a handler to get all texts from database.
    /// </summary>
    public record GetAllTextsQuery : IRequest<Result<IEnumerable<TextDto>>>;
}