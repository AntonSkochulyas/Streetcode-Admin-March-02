using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Timeline;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Timeline.HistoricalContext.Create
{
    public class CreateHistoricalContextHandler : IRequestHandler<CreateHistoricalContextCommand, Result<HistoricalContextDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public CreateHistoricalContextHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

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
