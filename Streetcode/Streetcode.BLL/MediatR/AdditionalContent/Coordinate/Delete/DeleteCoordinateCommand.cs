using FluentResults;
using MediatR;
using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;

namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Delete;

public record DeleteCoordinateCommand(int Id) : IRequest<Result<StreetcodeCoordinate>>;