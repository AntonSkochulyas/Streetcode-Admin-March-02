// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Art;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Art.GetAll;

/// <summary>
/// Query, that requests a handler to get all arts from database.
/// </summary>
public record GetAllArtsQuery : IRequest<Result<IEnumerable<ArtDto>>>
{
    public GetAllArtsQuery()
    {
    }
}
