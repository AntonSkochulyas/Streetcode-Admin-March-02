// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Audio.GetBaseAudio;

/// <summary>
/// Handler, that handles a process of getting base audio by given id.
/// </summary>
public class GetBaseAudioHandler : IRequestHandler<GetBaseAudioQuery, Result<MemoryStream>>
{
    // Blob storage
    private readonly IBlobService _blobStorage;

    // Repository wrapper
    private readonly IRepositoryWrapper _repositoryWrapper;

    // Logger
    private readonly ILoggerService _logger;

    // Parametric constructor
    public GetBaseAudioHandler(IBlobService blobService, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
    {
        _blobStorage = blobService;
        _repositoryWrapper = repositoryWrapper;
        _logger = logger;
    }

    /// <summary>
    /// Method, that gets a base audio by given id.
    /// </summary>
    /// <param name="request">
    /// Base audio id to get.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token, for cancelling operation, if it needed.
    /// </param>
    /// <returns>
    /// A MemoryStream, or error, if it was while getting process.
    /// </returns>
    public async Task<Result<MemoryStream>> Handle(GetBaseAudioQuery request, CancellationToken cancellationToken)
    {
        var audio = await _repositoryWrapper.AudioRepository.GetFirstOrDefaultAsync(a => a.Id == request.Id);

        if (audio is null)
        {
            string errorMsg = string.Format(MediaErrors.GetBaseAudioHandlerCanNotFindAnAudioWithGivenIdError, request.Id);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return _blobStorage.FindFileInStorageAsMemoryStream(audio.BlobName ?? "");
    }
}
