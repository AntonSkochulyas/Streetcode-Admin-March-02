// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent.Fact;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.Fact.GetAll
{
    /// <summary>
    /// Handler, that handles a process of getting all facts from database.
    /// </summary>
    public class GetAllFactsHandler : IRequestHandler<GetAllFactsQuery, Result<IEnumerable<FactDto>>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public GetAllFactsHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that get all facts from database.
        /// </summary>
        /// <param name="request">
        /// Request to get all facts from database.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A IEnumerable of FactDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<IEnumerable<FactDto>>> Handle(GetAllFactsQuery request, CancellationToken cancellationToken)
        {
            var facts = await _repositoryWrapper.FactRepository.GetAllAsync();

            if (facts is null)
            {
                string errorMsg = StreetcodeErrors.GetAllFactsHandlerCanNotFindAnyFactsError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<FactDto>>(facts));
        }
    }
}