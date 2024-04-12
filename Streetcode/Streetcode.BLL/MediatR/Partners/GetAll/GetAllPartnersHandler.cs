// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.Dto.Partners;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Partners.GetAll;

/// <summary>
/// Handler, that handles a process of getting all partners from database.
/// </summary>
public class GetAllPartnersHandler : IRequestHandler<GetAllPartnersQuery, Result<IEnumerable<PartnerDto>>>
{
    // Mapper
    private readonly IMapper _mapper;

    // Repository wrapper
    private readonly IRepositoryWrapper _repositoryWrapper;

    // Logger
    private readonly ILoggerService _logger;

    // Parametric constructor
    public GetAllPartnersHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Method, that get all partners from database.
    /// </summary>
    /// <param name="request">
    /// Request to get all partners from database.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token, for cancelling operation, if it needed.
    /// </param>
    /// <returns>
    /// A IEnumerable of PartnerDto, or error, if it was while getting process.
    /// </returns>
    public async Task<Result<IEnumerable<PartnerDto>>> Handle(GetAllPartnersQuery request, CancellationToken cancellationToken)
    {
        var partners = await _repositoryWrapper
            .PartnersRepository
            .GetAllAsync(
                include: p => p
                    .Include(pl => pl.PartnerSourceLinks)
                    .Include(p => p.Streetcodes));

        if (partners is null)
        {
            string errorMsg = PartnersErrors.GetAllPartnersHandlerCanNotFindAnyPartnersError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return Result.Ok(_mapper.Map<IEnumerable<PartnerDto>>(partners));
    }
}
