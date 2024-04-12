// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Audio;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Audio.GetByStreetcodeId;

/// <summary>
/// Query, that requests a handler to get an audio by streetcode id.
/// </summary>
/// <param name="StreetcodeId">
/// Audio streetcode id to get.
/// </param>
public record GetAudioByStreetcodeIdQuery(int StreetcodeId)
    : IRequest<Result<AudioDto>>;
