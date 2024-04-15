// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.GetAll
{
    /// <summary>
    /// Handler, that handles a process of get all infoblocks from database.
    /// </summary>
    public class GetAllInfoBlocksHandler : IRequestHandler<GetAllInfoBlocksQuery, Result<IEnumerable<InfoBlockDto>>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public GetAllInfoBlocksHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that get all infoblocks from database.
        /// </summary>
        /// <param name="request">
        /// Request to get all infoblock from database.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A IEnumerable of InfoBlockDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<IEnumerable<InfoBlockDto>>> Handle(GetAllInfoBlocksQuery request, CancellationToken cancellationToken)
        {
            var infoBlocks = await _repositoryWrapper.InfoBlockRepository.GetAllAsync();

            if (infoBlocks is null)
            {
                const string errorMsg = $"Cannot find any info blocks";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<InfoBlockDto>>(infoBlocks));
        }
    }
}
