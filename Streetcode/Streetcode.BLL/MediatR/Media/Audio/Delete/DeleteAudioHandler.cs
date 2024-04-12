﻿// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Audio.Delete;

/// <summary>
/// Hadnler, that handles a process of deleting an audio.
/// </summary>
public class DeleteAudioHandler : IRequestHandler<DeleteAudioCommand, Result<Unit>>
{
    // Repository wrapper
    private readonly IRepositoryWrapper _repositoryWrapper;

    // Blob service
    private readonly IBlobService _blobService;

    // Logger
    private readonly ILoggerService _logger;

    // Parametric constructor
    public DeleteAudioHandler(IRepositoryWrapper repositoryWrapper, IBlobService blobService, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _blobService = blobService;
        _logger = logger;
    }

    /// <summary>
    /// Method, that deletes an audio.
    /// </summary>
    /// <param name="request">
    /// Request with audio id to delete.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token, for cancelling operation, if it needed.
    /// </param>
    /// <returns>
    /// A Unit, or error, if it was while deleting process.
    /// </returns>
    public async Task<Result<Unit>> Handle(DeleteAudioCommand request, CancellationToken cancellationToken)
    {
        var audio = await _repositoryWrapper.AudioRepository.GetFirstOrDefaultAsync(a => a.Id == request.Id);

        if (audio is null)
        {
            string errorMsg = string.Format(MediaErrors.DeleteAudioHandlerCanNotFindAnAudioWithGivenCategoryIdError, request.Id);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        _repositoryWrapper.AudioRepository.Delete(audio);

        var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

        if (resultIsSuccess)
        {
            _blobService.DeleteFileInStorage(audio.BlobName);
        }

        if (resultIsSuccess)
        {
            _logger?.LogInformation($"DeleteAudioCommand handled successfully");
            return Result.Ok(Unit.Value);
        }
        else
        {
            string errorMsg = MediaErrors.DeleteAudioHandlerFailedToDeleteAnAudioError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }
    }
}
