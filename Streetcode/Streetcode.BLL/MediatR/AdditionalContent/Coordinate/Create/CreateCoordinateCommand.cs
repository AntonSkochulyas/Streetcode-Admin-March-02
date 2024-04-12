// Necessary usings
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent.Coordinates.Types;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create;

/// <summary>
/// Command, that creates a streetcode coordinate, and returns a StreetcodeCoordinateDto in json format, or error, if it was while creating process.
/// </summary>
/// <param name="StreetcodeCoordinate">
/// Data transfer object of streetcode coordinate.
/// </param>
public record CreateCoordinateCommand(StreetcodeCoordinateDto StreetcodeCoordinate)
    : IRequest<Result<StreetcodeCoordinateDto>>;
