// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Timeline;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Timeline.TimelineItem;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Delete
{
    /// <summary>
    /// Handler, that handles a process of deleting a timeline item.
    /// </summary>
    public class DeleteTimelineItemHandler : IRequestHandler<DeleteTimelineItemCommand, Result<TimelineItemDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public DeleteTimelineItemHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Method, that deletes a timeline item.
        /// </summary>
        /// <param name="request">
        /// Request with timeline item id to delete.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A TimelineItemDto, or error, if it was while deleting process.
        /// </returns>
        public async Task<Result<TimelineItemDto>> Handle(DeleteTimelineItemCommand request, CancellationToken cancellationToken)
        {
            var timelineToDelete = await _repositoryWrapper.TimelineRepository.GetItemBySpecAsync(new GetByIdTimelineItemSpec(request.Id));

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
