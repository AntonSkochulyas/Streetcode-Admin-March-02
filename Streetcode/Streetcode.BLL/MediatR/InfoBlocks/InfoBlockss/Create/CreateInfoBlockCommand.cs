// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Create
{
    /// <summary>
    /// Command, that reqquest a handler to create infoblock.
    /// </summary>
    /// <param name="newInfoBlock">
    /// New infoblock.
    /// </param>
    public record CreateInfoBlockCommand(InfoBlockDto? NewInfoBlock)
        : IRequest<Result<InfoBlockDto>>;
}
