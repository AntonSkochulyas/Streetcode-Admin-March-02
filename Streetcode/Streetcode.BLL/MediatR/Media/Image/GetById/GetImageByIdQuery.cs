// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Images;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Image.GetById;

/// <summary>
/// Query, that requests handler to get an image from database by given id.
/// </summary>
/// <param name="Id">
/// Image id to get.
/// </param>
public record GetImageByIdQuery(int Id) : IRequest<Result<ImageDto>>;
