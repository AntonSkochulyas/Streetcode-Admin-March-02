// Necessary usings
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent.Coordinates.Types;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate.GetByStreetcodeId
{
    /// <summary>
    /// Command, that get collection of StreetcodeCoordinateDto by id, or error, if it was while getting process.
    /// </summary>
    /// <param name="StreetcodeId">
    /// Param for finding elements.
    /// </param>
    public record GetCoordinatesByStreetcodeIdQuery(int StreetcodeId)
        : IRequest<Result<IEnumerable<StreetcodeCoordinateDto>>>;
}
