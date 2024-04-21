using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Timeline;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Timeline;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Timeline.TimelineItem;

namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Update
{
    public class UpdateTimelineItemHandler : IRequestHandler<UpdateTimelineItemCommand, Result<TimelineItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public UpdateTimelineItemHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<TimelineItemDto>> Handle(UpdateTimelineItemCommand request, CancellationToken cancellationToken)
        {
            DAL.Entities.Timeline.TimelineItem? timelineToUpdate = null;
            if (request.TimelineItem is not null)
            {
                timelineToUpdate = await _repositoryWrapper.TimelineRepository
                .GetItemBySpecAsync(new GetByIdTimelineItemIncludeSpec(request.TimelineItem.Id));
            }

            if (timelineToUpdate == null)
            {
                string errorMsg = string.Format(TimelineErrors.UpdateTimelineItemHandlerCanNotFindWIthGIvenIdError, request.TimelineItem?.Id);
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            await UpdateTimeline(timelineToUpdate, request.TimelineItem!);

            _repositoryWrapper.TimelineRepository.Update(timelineToUpdate);

            var isResultSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (isResultSuccess)
            {
                return Result.Ok(_mapper.Map<TimelineItemDto>(timelineToUpdate));
            }
            else
            {
                string errorMsg = TimelineErrors.UpdateTimelineItemHandlerFailedToUpdateError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }

        private async Task UpdateTimeline(DAL.Entities.Timeline.TimelineItem timelineToUpdate, TimelineItemDto timelineThatUpdate)
        {
            timelineToUpdate.Title = timelineThatUpdate.Title;
            timelineToUpdate.Description = timelineThatUpdate.Description;
            timelineToUpdate.Date = timelineThatUpdate.Date;
            timelineToUpdate.DateViewPattern = timelineThatUpdate.DateViewPattern;

            var oldHistoricalContextTimeline = timelineToUpdate.HistoricalContextTimelines.FirstOrDefault(x => x.TimelineId == timelineToUpdate.Id);

            if (oldHistoricalContextTimeline != null)
            {
                // Break many-to-many relationship
                _repositoryWrapper.HistoricalContextTimelineRepository.Delete(oldHistoricalContextTimeline);

                var newHistoricalContext = timelineThatUpdate.HistoricalContexts.FirstOrDefault();

                if (newHistoricalContext != null)
                {
                    var newHistoricalContextTimeline = new HistoricalContextTimeline() { TimelineId = timelineToUpdate.Id, HistoricalContextId = newHistoricalContext.Id };

                    await _repositoryWrapper.HistoricalContextTimelineRepository.CreateAsync(newHistoricalContextTimeline);

                    timelineToUpdate.HistoricalContextTimelines.Clear();
                    timelineToUpdate.HistoricalContextTimelines.Add(newHistoricalContextTimeline);
                }
            }
        }
    }
}