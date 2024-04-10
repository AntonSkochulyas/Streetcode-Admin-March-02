// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Update
{
    /// <summary>
    /// Command, that request a handler to update a authorship hyperlink.
    /// </summary>
    /// <param name="authorsHyperLink">
    /// Updated authorship hyperlink.
    /// </param>
    public record UpdateAuthorShipHyperLinkCommand(AuthorShipHyperLinkDto? authorsHyperLink) : IRequest<Result<AuthorShipHyperLinkDto>>;
}
