// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Audio;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Audio.GetAll;

/// <summary>
/// Handler, that handles a process of getting all audios from database.
/// </summary>
public class GetAllAudiosHandler : IRequestHandler<GetAllAudiosQuery, Result<IEnumerable<AudioDto>>>
{
    // Mapper
    private readonly IMapper _mapper;

    // Repository wrapper
    private readonly IRepositoryWrapper _repositoryWrapper;

    // Blob service
    private readonly IBlobService _blobService;

    // Logger
    private readonly ILoggerService _logger;

    // Parametric constructor 
    public GetAllAudiosHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, IBlobService blobService, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _blobService = blobService;
        _logger = logger;
    }

    /// <summary>
    /// Method, that gets all audios from database.
    /// </summary>
    /// <param name="request">
    /// Request to get all audios from database.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token, for cancelling operation, if it needed.
    /// </param>
    /// <returns>
    /// A IEnumerable of AudioDto, or error, if it was while getting process.
    /// </returns>
    public async Task<Result<IEnumerable<AudioDto>>> Handle(GetAllAudiosQuery request, CancellationToken cancellationToken)
    {
        var audios = await _repositoryWrapper.AudioRepository.GetAllAsync();

        if (audios is null)
        {
            string errorMsg = MediaErrors.GetAllAudiosHandlerCanNotFindAnyAudioError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        var audioDtos = _mapper.Map<IEnumerable<AudioDto>>(audios);
        foreach (var audio in audioDtos)
        {
            audio.Base64 = _blobService.FindFileInStorageAsBase64(audio.BlobName ?? "");
        }

        return Result.Ok(audioDtos);
    }
}