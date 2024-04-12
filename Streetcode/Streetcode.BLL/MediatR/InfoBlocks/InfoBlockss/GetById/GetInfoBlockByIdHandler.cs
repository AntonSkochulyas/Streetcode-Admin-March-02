// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.GetById
{
    /// <summary>
    /// Handler, that handles a process of getting infoblock by given id.
    /// </summary>
    public class GetInfoBlockByIdHandler : IRequestHandler<GetInfoBlockByIdQuery, Result<InfoBlockDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public GetInfoBlockByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that gets a infoblock by given id.
        /// </summary>
        /// <param name="request">
        /// Request with infoblock id to get.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A InfoBlockDto, or error, if it was while getting process.
        /// </returns>
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
