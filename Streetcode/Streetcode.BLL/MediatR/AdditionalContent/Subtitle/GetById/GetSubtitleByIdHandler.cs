// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent.Subtitles;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.AdditionalContent.GetById;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.AdditionalContent.Subtitle.GetById;

/// <summary>
/// Handler, that returns a SubtitleDto, or error, if it was while finding process.
/// </summary>
public class GetSubtitleByIdHandler : IRequestHandler<GetSubtitleByIdQuery, Result<SubtitleDto>>
{
    // Mapper
    private readonly IMapper _mapper;

    // Repository wrapper
    private readonly IRepositoryWrapper _repositoryWrapper;

    // Logger
    private readonly ILoggerService _logger;

    // Parametric constructor
    public GetSubtitleByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Method, that returns a SubtitleDto, or error, if it was while finding process.
    /// </summary>
    /// <param name="request">
    /// Request with id to find a model.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token for cancelling operation, if it needed.
    /// </param>
    /// <returns>
    /// A SubtitleDto, or error, if it was while finding process.
    /// </returns>
    public async Task<Result<SubtitleDto>> Handle(GetSubtitleByIdQuery request, CancellationToken cancellationToken)
    {
        var subtitle = await _repositoryWrapper.SubtitleRepository.GetFirstOrDefaultAsync(f => f.Id == request.Id);

        if (subtitle is null)
        {
            string errorMsg = SubtitleErrors.GetSubtitleByIdHandlerCanNotFindByIdError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return Result.Ok(_mapper.Map<SubtitleDto>(subtitle));
    }
}