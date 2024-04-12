using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks;

namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Create
{
    public record CreateInfoBlockCommand(InfoBlockDto? NewInfoBlock)
        : IRequest<Result<InfoBlockDto>>;
}
