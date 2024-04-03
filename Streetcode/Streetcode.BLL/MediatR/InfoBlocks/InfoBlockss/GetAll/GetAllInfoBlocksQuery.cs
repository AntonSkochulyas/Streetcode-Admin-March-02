using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks;

namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.GetAll
{
    public record GetAllInfoBlocksQuery : IRequest<Result<IEnumerable<InfoBlockDto>>>;
}
