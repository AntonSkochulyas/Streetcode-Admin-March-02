// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Audio;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Audio.GetById;

/// <summary>
/// Query, that requests a handler to get an audio by given id.
/// </summary>
/// <param name="Id">
/// Audio id to get.
/// </param>
public record GetAudioByIdQuery(int Id) : IRequest<Result<AudioDto>>;
