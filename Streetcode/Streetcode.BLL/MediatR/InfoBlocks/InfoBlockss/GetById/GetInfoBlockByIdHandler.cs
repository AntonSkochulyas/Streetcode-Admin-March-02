using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.GetById
{
    public class GetInfoBlockByIdHandler : IRequestHandler<GetInfoBlockByIdQuery, Result<InfoBlockDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetInfoBlockByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<InfoBlockDto>> Handle(GetInfoBlockByIdQuery request, CancellationToken cancellationToken)
        {
            var infoBlock = await _repositoryWrapper.InfoBlockRepository.GetFirstOrDefaultAsync(i => i.Id == request.Id);

            if (infoBlock is null)
            {
                string errorMsg = $"Cannot find a info block with corresponding id: {request.Id}";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<InfoBlockDto>(infoBlock));
        }
    }
}
