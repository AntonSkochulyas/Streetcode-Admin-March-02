// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Audio;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Audio.Delete;

/// <summary>
/// Command, that requests a handler to delete an audio.
/// </summary>
/// <param name="Id">
/// Audio id to delete.
/// </param>
public record DeleteAudioCommand(int Id)
    : IRequest<Result<AudioDto>>;
