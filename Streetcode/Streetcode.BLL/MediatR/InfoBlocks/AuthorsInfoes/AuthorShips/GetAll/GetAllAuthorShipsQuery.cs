using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.GetAll
{
    public record GetAllAuthorShipsQuery : IRequest<Result<IEnumerable<AuthorShipDto>>>;
}
