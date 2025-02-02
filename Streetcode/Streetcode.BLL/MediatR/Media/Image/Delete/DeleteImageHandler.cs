﻿// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.Dto.Media.Images;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Image.Delete;

/// <summary>
/// Handler, that handles a process of deleting an image.
/// </summary>
public class DeleteImageHandler : IRequestHandler<DeleteImageCommand, Result<ImageDto>>
{
    // Repository wrapper
    private readonly IRepositoryWrapper _repositoryWrapper;

    // Blob service
    private readonly IBlobService _blobService;

    // Logger
    private readonly ILoggerService _logger;

    // Mapper
    private readonly IMapper _mapper;

    // Parametric constructor 
    public DeleteImageHandler(IRepositoryWrapper repositoryWrapper, IBlobService blobService, ILoggerService logger, IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _blobService = blobService;
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Method, that deletes an image with given id.
    /// </summary>
    /// <param name="request">
    /// Request with image id to delete.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token, for cancelling operation, if it needed.
    /// </param>
    /// <returns>
    /// A Unit, or error, if it was while deleting process.
    /// </returns>
    public async Task<Result<ImageDto>> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _repositoryWrapper.ImageRepository
            .GetFirstOrDefaultAsync(
            predicate: i => i.Id == request.Id,
            include: s => s.Include(i => i.Streetcodes));

        if (image is null)
        {
            string errorMsg = string.Format(MediaErrors.DeleteImageHandlerCanNotFindAnImageWithGivenCategoryIdError, request.Id);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        _repositoryWrapper.ImageRepository.Delete(image);

        var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

        if (resultIsSuccess)
        {
            _blobService.DeleteFileInStorage(image.BlobName);
        }

        if (resultIsSuccess)
        {
            return Result.Ok(_mapper.Map<ImageDto>(image));
        }
        else
        {
            string errorMsg = MediaErrors.DeleteImageHandlerFailedToDeleteAnImageError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }
    }
}