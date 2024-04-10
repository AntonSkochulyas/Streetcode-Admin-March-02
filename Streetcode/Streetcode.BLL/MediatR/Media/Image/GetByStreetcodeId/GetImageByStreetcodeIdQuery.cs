// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Images;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Image.GetByStreetcodeId;

/// <summary>
/// Query, that request a handler to get an image by streetcode id.
/// </summary>
/// <param name="StreetcodeId">
/// Image streetcode id to get.
/// </param>
public record GetImageByStreetcodeIdQuery(int StreetcodeId) : IRequest<Result<IEnumerable<ImageDto>>>;