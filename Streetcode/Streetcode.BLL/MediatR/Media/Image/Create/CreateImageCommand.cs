// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Images;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Image.Create;

/// <summary>
/// Command, that requests a handler to create a new image.
/// </summary>
/// <param name="Image">
/// New image.
/// </param>
public record CreateImageCommand(ImageFileBaseCreateDto Image)
    : IRequest<Result<ImageDto>>;
