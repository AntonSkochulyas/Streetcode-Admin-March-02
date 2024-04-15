// Necessary usings
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent.Coordinates.Types;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.AdditionalContent.Coordinate.GetByStreetcodeId;

/// <summary>
/// Handler, that returns a StreetcodeCoordinateDto`s.
/// </summary>
public class GetCoordinatesByStreetcodeIdHandler : IRequestHandler<GetCoordinatesByStreetcodeIdQuery, Result<IEnumerable<StreetcodeCoordinateDto>>>
{
    // Mapper
    private readonly IMapper _mapper;

    // Repository wrapper
    private readonly IRepositoryWrapper _repositoryWrapper;

    // Logger
    private readonly ILoggerService _logger;

    // Parametric constructor
    public GetCoordinatesByStreetcodeIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Method, that returns a collection of StreetcodeCoordinateDto`s by id, or error, if it was while getting process.
    /// </summary>
    /// <param name="request"> 
    /// Request with id.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token for cancel operation.
    /// </param>
    /// <returns>
    /// IEnumerable of StreetcodeCoordinateDto, or error.
    /// </returns>
    public async Task<Result<IEnumerable<StreetcodeCoordinateDto>>> Handle(GetCoordinatesByStreetcodeIdQuery request, CancellationToken cancellationToken)
    {
        if ((await _repositoryWrapper.StreetcodeRepository.GetFirstOrDefaultAsync(s => s.Id == request.StreetcodeId)) is null)
        {
            return Result.Fail(
                new Error(string.Format(CoordinateErrors.GetCoordinatesByStreetcodeIdHandlerCanNotFindCoordinatesStreetcodeNotExist, request.StreetcodeId)));
        }

        var coordinates = await _repositoryWrapper.StreetcodeCoordinateRepository
            .GetAllAsync(c => c.StreetcodeId == request.StreetcodeId);

        if (coordinates is null)
        {
            string errorMsg = CoordinateErrors.GetCoordinatesByStreetcodeIdHandlerCanNotFindCoordinatesByStreetcodeId;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return Result.Ok(_mapper.Map<IEnumerable<StreetcodeCoordinateDto>>(coordinates));
    }
}
