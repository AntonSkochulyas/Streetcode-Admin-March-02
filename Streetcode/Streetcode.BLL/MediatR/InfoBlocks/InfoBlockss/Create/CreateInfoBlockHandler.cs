using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.InfoBlocks;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Create
{
    public class CreateInfoBlockHandler : IRequestHandler<CreateInfoBlockCommand, Result<InfoBlockDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public CreateInfoBlockHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<InfoBlockDto>> Handle(CreateInfoBlockCommand request, CancellationToken cancellationToken)
        {
            var newInfoBlock = _mapper.Map<InfoBlock>(request.newInfoBlock);

            if (newInfoBlock is null)
            {
                const string errorMsg = "Cannot convert null to info block";

                _logger.LogError(request, errorMsg);

                return Result.Fail(errorMsg);
            }

            var entity = _repositoryWrapper.InfoBlockRepository.Create(newInfoBlock);

            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(_mapper.Map<InfoBlockDto>(entity));
            }
            else
            {
                const string errorMsg = "Failed to create a info block";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
