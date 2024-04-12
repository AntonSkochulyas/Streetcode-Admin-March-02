// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Video;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Video.GetById;

/// <summary>
/// Query, that requests a handler to get a video from database by given id.
/// </summary>
/// <param name="Id">
/// Video id to get.
/// </param>
public record GetVideoByIdQuery(int Id) : IRequest<Result<VideoDto>>;
