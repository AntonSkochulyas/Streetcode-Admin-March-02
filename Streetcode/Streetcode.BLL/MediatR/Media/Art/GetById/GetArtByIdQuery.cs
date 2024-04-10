// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Art;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Art.GetById;

/// <summary>
/// Query, that requests a handler to get art by given id.
/// </summary>
/// <param name="Id">
/// Art id to get.
/// </param>
public record GetArtByIdQuery(int Id) : IRequest<Result<ArtDto>>;
