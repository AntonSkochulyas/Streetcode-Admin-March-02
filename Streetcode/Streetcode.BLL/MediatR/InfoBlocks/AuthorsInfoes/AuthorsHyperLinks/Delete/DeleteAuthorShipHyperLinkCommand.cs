using FluentResults;
using MediatR;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Delete
{
    public record DeleteAuthorShipHyperLinkCommand(int Id)
        : IRequest<Result<Unit>>;
}
