// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.GetAll
{
    /// <summary>
    /// Query, that request a handler to get all authorship from database.
    /// </summary>
    public record GetAllAuthorShipsQuery : IRequest<Result<IEnumerable<AuthorShipDto>>>
    {
        public GetAllAuthorShipsQuery()
        {
        }
    }
}
