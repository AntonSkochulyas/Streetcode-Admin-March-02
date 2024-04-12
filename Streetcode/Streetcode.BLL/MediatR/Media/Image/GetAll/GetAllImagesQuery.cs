// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Images;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Image.GetAll;

/// <summary>
/// Query, that requests a handler to get all images from database.
/// </summary>
public record GetAllImagesQuery : IRequest<Result<IEnumerable<ImageDto>>>;