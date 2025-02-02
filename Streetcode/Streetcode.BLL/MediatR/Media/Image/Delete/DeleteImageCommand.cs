﻿// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Images;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Image.Delete;

/// <summary>
/// Command, that requests handler to delete an image with given id.
/// </summary>
/// <param name="Id">
/// Image id to delete.
/// </param>
public record DeleteImageCommand(int Id)
    : IRequest<Result<ImageDto>>;
