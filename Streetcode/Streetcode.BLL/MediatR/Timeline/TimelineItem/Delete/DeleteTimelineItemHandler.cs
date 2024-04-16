using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Timeline;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Delete
{
    public class DeleteTimelineItemHandler : IRequestHandler<DeleteTimelineItemCommand, Result<TimelineItemDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public DeleteTimelineItemHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<TimelineItemDto>> Handle(DeleteTimelineItemCommand request, CancellationToken cancellationToken)
        {
            var timelineToDelete = await _repositoryWrapper.TimelineRepository.GetFirstOrDefaultAsync(x => x.Id == request.Id);

            if (timelineToDelete == null)
            {
                string errorMsg = string.Format(TimelineErrors.DeleteTimelineItemHandlerCanNotFindWithIdError, request.Id);
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            _repositoryWrapper.TimelineRepository.Delete(timelineToDelete);

            var isResultSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (isResultSuccess)
            {
                return Result.Ok(_mapper.Map<TimelineItemDto>(timelineToDelete));
            }
            else
            {
                string errorMsg = TimelineErrors.DeleteTimelineItemHandlerFailedToDeleteError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
