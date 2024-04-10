// Necesasry usings.
using FluentResults;
using MediatR;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Image.GetBaseImage;

/// <summary>
/// Query, that requests a handler to get base image from database by given id.
/// </summary>
/// <param name="Id">
/// Base image id to get.
/// </param>
public record GetBaseImageQuery(int Id) : IRequest<Result<MemoryStream>>;