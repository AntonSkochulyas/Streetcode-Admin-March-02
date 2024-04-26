// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Dto.Media.Art;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Art.GetAll;

/// <summary>
/// Handler, that handles a process of getting all arts from database.
/// </summary>
public class GetAllArtsHandler : IRequestHandler<GetAllArtsQuery, Result<IEnumerable<ArtDto>>>
{
    // Mapper
    private readonly IMapper _mapper;

    // Repository wrapper
    private readonly IRepositoryWrapper _repositoryWrapper;

    // Logger
    private readonly ILoggerService _logger;

    // Parametric constructor
    public GetAllArtsHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Method, that gets all arts from database.
    /// </summary>
    /// <param name="request">
    /// Request to get all posts from database;
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token, for cancelling operation, if it needed.
    /// </param>
    /// <returns>
    /// A IEnumerable of ArtDto, or error, if it was while getting process.
    /// </returns>
    public async Task<Result<IEnumerable<ArtDto>>> Handle(GetAllArtsQuery request, CancellationToken cancellationToken)
    {
        var arts = await _repositoryWrapper.ArtRepository.GetAllAsync();

        if (arts is null)
        {
            string errorMsg = MediaErrors.GetAllArtsHandlerCanNotFindAnyArtsError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return Result.Ok(_mapper.Map<IEnumerable<ArtDto>>(arts));
    }
}