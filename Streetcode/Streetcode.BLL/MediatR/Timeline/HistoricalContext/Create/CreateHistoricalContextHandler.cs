// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Timeline;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Timeline.HistoricalContext.Create
{
    /// <summary>
    /// Handler, that handles a process of creating a new historical context.
    /// </summary>
    public class CreateHistoricalContextHandler : IRequestHandler<CreateHistoricalContextCommand, Result<HistoricalContextDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public CreateHistoricalContextHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that creates a new historical context.
        /// </summary>
        /// <param name="request">
        /// Request with a new historical context.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A HistoricalContextDto, or error, if it was while creating process.
        /// </returns>
        public async Task<Result<HistoricalContextDto>> Handle(CreateHistoricalContextCommand request, CancellationToken cancellationToken)
        {
            var newHistoricalContext = _mapper.Map<DAL.Entities.Timeline.HistoricalContext>(request.NewHistoricalContext);

            if (newHistoricalContext == null)
            {
                string errorMsg = TimelineErrors.CreateHistoricalContextHandlerCanNotConvertFromNullError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var createdEntity = _repositoryWrapper.HistoricalContextRepository.Create(newHistoricalContext);
            var isResultSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (isResultSuccess)
            {
                return Result.Ok(_mapper.Map<HistoricalContextDto>(createdEntity));
            }
            else
            {
                string errorMsg = TimelineErrors.CreateHistoricalContextHandlerFailedToCreateError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
