// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.GetById
{
    /// <summary>
    /// Query, that requests a handler to get infoblock by given id.
    /// </summary>
    /// <param name="Id">
    /// Infoblock id to get.
    /// </param>
    public record GetInfoBlockByIdQuery(int Id) : IRequest<Result<InfoBlockDto>>;
}
