// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Timeline;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Timeline;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Create
{
    /// <summary>
    /// Handler, that handles a process of creating a new timeline item.
    /// </summary>
    public class CreateTimelineItemHandler : IRequestHandler<CreateTimelineItemCommand, Result<TimelineItemDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public CreateTimelineItemHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that creates a new timeline item.
        /// </summary>
        /// <param name="request">
        /// Request with a new timeline item.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A TimelineItemDto, or error, if it was while creating process.
        /// </returns>
        public async Task<Result<TimelineItemDto>> Handle(CreateTimelineItemCommand request, CancellationToken cancellationToken)
        {
            DAL.Entities.Timeline.TimelineItem? newTimelineItem = null;

            if (request.TimelineItem is not null)
            {
                newTimelineItem = _mapper.Map<DAL.Entities.Timeline.TimelineItem>(request.TimelineItem);
            }

            if (newTimelineItem == null)
            {
                string errorMsg = TimelineErrors.CreateTimelineItemHandlerCanNotConvertFromNullError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            if (request.TimelineItem?.HistoricalContexts is null)
            {
                string errorMsg = TimelineErrors.CreateTimelineItemHandlerHistoricalContextsIsNullError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var newHistoricalContextTimeline = new HistoricalContextTimeline()
            {
                HistoricalContextId = request.TimelineItem.HistoricalContexts.First().Id,
                TimelineId = newTimelineItem.Id
            };

            await _repositoryWrapper.HistoricalContextTimelineRepository.CreateAsync(newHistoricalContextTimeline);

            newTimelineItem.HistoricalContextTimelines.Add(newHistoricalContextTimeline);
            newTimelineItem.StreetcodeId = 1;

            var createdTimeline = _repositoryWrapper.TimelineRepository.Create(newTimelineItem);
            var isCreatedSuccessfully = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (isCreatedSuccessfully)
            {
                return Result.Ok(_mapper.Map<TimelineItemDto>(createdTimeline));
            }
            else
            {
                string errorMsg = TimelineErrors.CreateTimelineItemHandlerFailedToCreateError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
