using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.GetAll
{
    public class GetAllInfoBlocksHandler : IRequestHandler<GetAllInfoBlocksQuery, Result<IEnumerable<InfoBlockDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetAllInfoBlocksHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<InfoBlockDto>>> Handle(GetAllInfoBlocksQuery request, CancellationToken cancellationToken)
        {
            var infoBlocks = await _repositoryWrapper.InfoBlockRepository.GetAllAsync();

            if (infoBlocks is null)
            {
                const string errorMsg = $"Cannot find any info blocks";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<InfoBlockDto>>(infoBlocks));
        }
    }
}
