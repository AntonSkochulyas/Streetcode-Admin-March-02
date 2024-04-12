// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Video;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Video.GetAll;

/// <summary>
/// Query, that requests a handler to get all videos from database.
/// </summary>
public record GetAllVideosQuery : IRequest<Result<IEnumerable<VideoDto>>>;