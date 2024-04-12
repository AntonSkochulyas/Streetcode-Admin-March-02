// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Create
{
    /// <summary>
    /// Command, that request handler to create a new author hyperlink.
    /// </summary>
    /// <param name="newAuthorHyperLink">
    /// New author hyper link.
    /// </param>
    public record CreateAuthorShipHyperLinkCommand(AuthorShipHyperLinkDto? NewAuthorHyperLink)
        : IRequest<Result<AuthorShipHyperLinkDto>>;
}
