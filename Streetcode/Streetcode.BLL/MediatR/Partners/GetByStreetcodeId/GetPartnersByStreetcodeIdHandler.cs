// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.Dto.Partners;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespace.
namespace Streetcode.BLL.MediatR.Partners.GetByStreetcodeId;

/// <summary>
/// Handler, that handles a process of getting a partners by given streetcode id.
/// </summary>
public class GetPartnersByStreetcodeIdHandler : IRequestHandler<GetPartnersByStreetcodeIdQuery, Result<IEnumerable<PartnerDto>>>
{
    // Mapper
    private readonly IMapper _mapper;

    // Repository wrapper
    private readonly IRepositoryWrapper _repositoryWrapper;

    // Logger
    private readonly ILoggerService _logger;

    // Parametric constructor 
    public GetPartnersByStreetcodeIdHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
        _logger = logger;
    }

    /// <summary>
    /// Method, that gets a partners by given streetcode id.
    /// </summary>
    /// <param name="request">
    /// Request with partner id to get.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token, for cancelling operation, if it needed.
    /// </param>
    /// <returns>
    /// A IEnumerable of PartnerDto, or error, if it was while getting process.
    /// </returns>
    public async Task<Result<IEnumerable<PartnerDto>>> Handle(GetPartnersByStreetcodeIdQuery request, CancellationToken cancellationToken)
    {
        var streetcode = await _repositoryWrapper.StreetcodeRepository
            .GetSingleOrDefaultAsync(st => st.Id == request.StreetcodeId);

        if (streetcode is null)
        {
            string errorMsg = string.Format(PartnersErrors.GetPartnersByStreetcodeIdHandlerCanNotFindAnyPartnersWithGivenStreetcodeId, request.StreetcodeId);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        var partners = await _repositoryWrapper.PartnersRepository
            .GetAllAsync(
                predicate: p => p.Streetcodes.Any(sc => sc.Id == streetcode.Id) || p.IsVisibleEverywhere,
                include: p => p.Include(pl => pl.PartnerSourceLinks));

        if (partners is null)
        {
            string errorMsg = string.Format(PartnersErrors.GetPartnersByStreetcodeIdHandlerCanNotFindAPartnersWithGivenStreetcodeIdError, request.StreetcodeId);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return Result.Ok(value: _mapper.Map<IEnumerable<PartnerDto>>(partners));
    }
}
