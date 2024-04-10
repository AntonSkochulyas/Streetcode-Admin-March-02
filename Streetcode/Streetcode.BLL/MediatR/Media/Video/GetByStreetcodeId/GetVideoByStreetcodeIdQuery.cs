// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Video;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Video.GetByStreetcodeId;

/// <summary>
/// Query, that requests a handler to get a video by given streetcode id.
/// </summary>
/// <param name="StreetcodeId">
/// Streetcode id video to get.
/// </param>
public record GetVideoByStreetcodeIdQuery(int StreetcodeId) : IRequest<Result<VideoDto>>;