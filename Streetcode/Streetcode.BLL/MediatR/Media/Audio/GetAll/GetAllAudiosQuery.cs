// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Audio;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Audio.GetAll;

/// <summary>
/// Query, that request a handler to get all audios from database.
/// </summary>
public record GetAllAudiosQuery : IRequest<Result<IEnumerable<AudioDto>>>
{
    public GetAllAudiosQuery()
    {
    }
}
