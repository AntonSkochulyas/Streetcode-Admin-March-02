// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Update
{
    /// <summary>
    /// Command, that requests a handler to update an infoblock.
    /// </summary>
    /// <param name="infoBlock">
    /// Updated infoblock.
    /// </param>
    public record UpdateInfoBlockCommand(InfoBlockDto? infoBlock) : IRequest<Result<InfoBlockDto>>;
}
