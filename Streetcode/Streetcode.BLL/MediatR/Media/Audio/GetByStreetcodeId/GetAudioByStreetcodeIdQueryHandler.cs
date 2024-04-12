// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.Dto.Media.Audio;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.MediatR.ResultVariations;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Audio.GetByStreetcodeId;

/// <summary>
/// Handler, that handles a process of getting an audio by streetcode id.
/// </summary>
public class GetAudioByStreetcodeIdQueryHandler : IRequestHandler<GetAudioByStreetcodeIdQuery, Result<AudioDto>>
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
    public GetAudioByStreetcodeIdQueryHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, IBlobService blobService, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _blobService = blobService;
        _logger = logger;
    }

    /// <summary>
    /// Method, that gets an audio by given streetcode id.
    /// </summary>
    /// <param name="request">
    /// Request with audio streetcode id to get.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token, for cancelling operation, if it needed.
    /// </param>
    /// <returns>
    /// A AudioDto, or error, if it was while getting process.
    /// </returns>
    public async Task<Result<AudioDto>> Handle(GetAudioByStreetcodeIdQuery request, CancellationToken cancellationToken)
    {
        var streetcode = await _repositoryWrapper.StreetcodeRepository.GetFirstOrDefaultAsync(
            s => s.Id == request.StreetcodeId,
            include: q => q.Include(s => s.Audio) !);
        if (streetcode == null)
        {
            string errorMsg = string.Format(MediaErrors.GetAudioByStreetcodeIdQueryHandlerCanNotFindAnAudioWithGivenStreetcodeIdError, request.StreetcodeId);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        NullResult<AudioDto> result = new NullResult<AudioDto>();

        if (streetcode.Audio != null)
        {
            AudioDto audioDto = _mapper.Map<AudioDto>(streetcode.Audio);
            audioDto = _mapper.Map<AudioDto>(streetcode.Audio);
            audioDto.Base64 = _blobService.FindFileInStorageAsBase64(audioDto.BlobName);
            result.WithValue(audioDto);
        }

        return result;
    }
}