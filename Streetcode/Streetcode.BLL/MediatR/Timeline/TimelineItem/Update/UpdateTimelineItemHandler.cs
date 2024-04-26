using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Timeline;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Team;
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
            if(request.TimelineItem == null)
            {
                string errorMsg = TimelineErrors.UpdateTimelineItemHandlerFailedToUpdateError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            // if (await _repositoryWrapper.StreetcodeRepository.GetItemBySpecAsync(new GetByIdStreetcodeSpec(request.TimelineItem.StreetcodeId)) == null)
            if (await _repositoryWrapper.StreetcodeRepository.GetFirstOrDefaultAsync(s => s.Id == request.TimelineItem.StreetcodeId) == null)
            {
                string errorMsg = TimelineErrors.UpdateTimelineItemHandlerFailedToUpdateError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            // DAL.Entities.Timeline.TimelineItem? timelineToUpdate = await _repositoryWrapper.TimelineRepository
            //        .GetItemBySpecAsync(new GetByIdTimelineItemSpec(request.TimelineItem.Id));

            DAL.Entities.Timeline.TimelineItem? timelineToUpdate = await _repositoryWrapper.TimelineRepository
                    .GetFirstOrDefaultAsync(t => t.Id == request.TimelineItem.Id);

            if (timelineToUpdate == null)
            {
                string errorMsg = string.Format(TimelineErrors.UpdateTimelineItemHandlerCanNotFindWIthGIvenIdError, request.TimelineItem?.Id);
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

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
    }
}