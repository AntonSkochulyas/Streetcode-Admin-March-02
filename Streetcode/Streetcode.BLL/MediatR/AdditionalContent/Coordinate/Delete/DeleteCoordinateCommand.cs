// Necessary usings
using FluentResults;
using MediatR;
using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Delete;

/// <summary>
/// Command, that deletes a StreetcodeCoordinate by id, and returns a deleted StreetcodeCoordinate, or error, if it was while deleting process.
/// </summary>
/// <param name="Id"></param>
public record DeleteCoordinateCommand(int Id) : IRequest<Result<StreetcodeCoordinate>>;