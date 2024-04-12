using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks;

namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Update
{
    public record UpdateInfoBlockCommand(InfoBlockDto? InfoBlock)
        : IRequest<Result<InfoBlockDto>>;
}
