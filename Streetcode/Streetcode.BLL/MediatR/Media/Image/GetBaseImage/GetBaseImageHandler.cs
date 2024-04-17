// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Image.GetBaseImage;

/// <summary>
/// Handler, that handles a process of getting base image by given id from database.
/// </summary>
public class GetBaseImageHandler : IRequestHandler<GetBaseImageQuery, Result<MemoryStream>>
{
    // Blob storage
    private readonly IBlobService _blobStorage;

    // Repository wrapper
    private readonly IRepositoryWrapper _repositoryWrapper;

    // Logger
    private readonly ILoggerService _logger;

    // Parametric constructor
    public GetBaseImageHandler(IBlobService blobService, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
    {
        _blobStorage = blobService;
        _repositoryWrapper = repositoryWrapper;
        _logger = logger;
    }

    /// <summary>
    /// Method, that gets an base image by giveen id.
    /// </summary>
    /// <param name="request">
    /// Base image id to get.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token, for cancelling operation, if it needed.
    /// </param>
    /// <returns>
    /// A MemoryStream, or error, if it was while getting process.
    /// </returns>
    public async Task<Result<MemoryStream>> Handle(GetBaseImageQuery request, CancellationToken cancellationToken)
    {
        var image = await _repositoryWrapper.ImageRepository.GetFirstOrDefaultAsync(a => a.Id == request.Id);

        if (image is null)
        {
            string errorMsg = string.Format(MediaErrors.GetBaseImageHandlerCanNotFindAnAudioWithGivenIdError, request.Id);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return _blobStorage.FindFileInStorageAsMemoryStream(image.BlobName);
    }
}
