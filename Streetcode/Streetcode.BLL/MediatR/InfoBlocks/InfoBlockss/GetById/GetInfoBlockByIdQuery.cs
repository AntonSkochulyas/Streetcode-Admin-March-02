using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks;

namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.GetById
{
    public record GetInfoBlockByIdQuery(int Id) : IRequest<Result<InfoBlockDto>>;
}
