// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.GetById
{
    /// <summary>
    /// Query, that request a handler to get an authorship hyperlink by given id.
    /// </summary>
    /// <param name="Id">
    /// Authorship hyperlink id to get.
    /// </param>
    public record GetAuthorShipHyperLinksByIdQuery(int Id) : IRequest<Result<AuthorShipHyperLinkDto>>;
}
