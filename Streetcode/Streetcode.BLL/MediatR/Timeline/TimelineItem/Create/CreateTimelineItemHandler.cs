using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Timeline;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Timeline;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Create
{
    public class CreateTimelineItemHandler : IRequestHandler<CreateTimelineItemCommand, Result<TimelineItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public CreateTimelineItemHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<TimelineItemDto>> Handle(CreateTimelineItemCommand request, CancellationToken cancellationToken)
        {
            DAL.Entities.Timeline.TimelineItem? newTimelineItem = null;

            if (request.TimelineItem is not null)
            {
                newTimelineItem = _mapper.Map<DAL.Entities.Timeline.TimelineItem>(request.TimelineItem);
            }

            if (newTimelineItem == null)
            {
                const string errorMsg = "Can not convert null to timeline item.";
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            if (request.TimelineItem?.HistoricalContexts is null)
            {
                const string errorMsg = "HistoricalContexts is null";
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
                const string errorMsg = "Failed to create a timeline item.";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
