// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Art;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Art.GetByStreetcodeId
{
    /// <summary>
    /// Query, that requests a handler to get art from database by given streetcode id.
    /// </summary>
    /// <param name="StreetcodeId"></param>
    public record GetArtsByStreetcodeIdQuery(int StreetcodeId)
        : IRequest<Result<IEnumerable<ArtDto>>>;
}
