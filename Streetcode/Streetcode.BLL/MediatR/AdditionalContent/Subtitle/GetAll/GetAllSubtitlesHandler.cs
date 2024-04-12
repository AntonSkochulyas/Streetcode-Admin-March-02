// Necessary usings
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent.Subtitles;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.AdditionalContent.Subtitle.GetAll;

/// <summary>
/// Handler, that returns a collection of all subtitles, or error, if it was while getting process.
/// </summary>
public class GetAllSubtitlesHandler : IRequestHandler<GetAllSubtitlesQuery, Result<IEnumerable<SubtitleDto>>>
{
    // Mapper
    private readonly IMapper _mapper;

    // Repository wrapper
    private readonly IRepositoryWrapper _repositoryWrapper;

    // Logger
    private readonly ILoggerService _logger;

    // Parametric constructor 
    public GetAllSubtitlesHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Method, that returns a collection of all subtitles, or error, if it was while getting process.
    /// </summary>
    /// <param name="request">
    /// Request to get all subtitles.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token for cancelling operation, if it needed.
    /// </param>
    /// <returns>
    /// A IEnumerable of SubtitleDto, or error, if it was while getting process.
    /// </returns>
    public async Task<Result<IEnumerable<SubtitleDto>>> Handle(GetAllSubtitlesQuery request, CancellationToken cancellationToken)
    {
        var subtitles = await _repositoryWrapper.SubtitleRepository.GetAllAsync();

        if (subtitles is null)
        {
            string errorMsg = SubtitleErrors.GetAllSubtitlesHandlerCanNotFindAnySubtitlesError;

            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return Result.Ok(_mapper.Map<IEnumerable<SubtitleDto>>(subtitles));
    }
}