// Necessary usings.
using FluentResults;
using MediatR;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Audio.GetBaseAudio;

/// <summary>
/// Query, that requests a handler to get base audio from database by given id.
/// </summary>
/// <param name="Id">
/// Base audio id to get.
/// </param>
public record GetBaseAudioQuery(int Id) : IRequest<Result<MemoryStream>>;
