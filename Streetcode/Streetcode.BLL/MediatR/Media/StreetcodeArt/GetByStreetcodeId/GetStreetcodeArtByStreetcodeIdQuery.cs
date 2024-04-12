// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Art;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.StreetcodeArt.GetByStreetcodeId
{
    /// <summary>
    /// Query, that requests a handler to get art by streetcode id.
    /// </summary>
    /// <param name="StreetcodeId">
    /// Streetcode id art to get.
    /// </param>
    public record GetStreetcodeArtByStreetcodeIdQuery(int StreetcodeId)
        : IRequest<Result<IEnumerable<StreetcodeArtDto>>>;
}
