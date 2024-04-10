// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.GetAll
{
    /// <summary>
    /// Query, that requests a handler to get all infoblocks from database.
    /// </summary>
    public record GetAllInfoBlocksQuery : IRequest<Result<IEnumerable<InfoBlockDto>>>;
}
