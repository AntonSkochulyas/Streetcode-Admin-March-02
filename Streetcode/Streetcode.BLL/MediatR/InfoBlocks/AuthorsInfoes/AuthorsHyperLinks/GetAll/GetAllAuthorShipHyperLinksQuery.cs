// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.GetAll
{
    /// <summary>
    /// Query, that request handler to get all authorship hyperlinks from database.
    /// </summary>
    public record GetAllAuthorShipHyperLinksQuery : IRequest<Result<IEnumerable<AuthorShipHyperLinkDto>>>
    {
        public GetAllAuthorShipHyperLinksQuery()
        {
        }
    }
}
