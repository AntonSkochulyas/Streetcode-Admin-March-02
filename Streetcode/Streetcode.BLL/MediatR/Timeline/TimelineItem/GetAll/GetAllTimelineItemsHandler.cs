// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Timeline;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Timeline.TimelineItem;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.GetAll
{
    /// <summary>
    /// Handler, that handles a process of getting all partners from database.
    /// </summary>
    public class GetAllTimelineItemsHandler : IRequestHandler<GetAllTimelineItemsQuery, Result<IEnumerable<TimelineItemDto>>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public GetAllTimelineItemsHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
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
        public async Task<Result<IEnumerable<TimelineItemDto>>> Handle(GetAllTimelineItemsQuery request, CancellationToken cancellationToken)
        {
            var timelineItems = await _repositoryWrapper.TimelineRepository.GetItemsBySpecAsync(new GetAllTimelineItemSpec());

            if (timelineItems is null)
            {
                string errorMsg = TimelineErrors.GetAllTimelineItemsHandlerCanNotFindAnyError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<TimelineItemDto>>(timelineItems));
        }
    }
}