// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Audio;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Audio.Create;

/// <summary>
/// Command, that request a handler to create an audio.
/// </summary>
/// <param name="Audio"></param>
public record CreateAudioCommand(AudioFileBaseCreateDto Audio)
    : IRequest<Result<AudioDto>>;
