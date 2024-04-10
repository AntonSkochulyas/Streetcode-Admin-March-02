// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.InfoBlocks;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Create
{
    /// <summary>
    /// Handler, that handles a process of creating an infoblock.
    /// </summary>
    public class CreateInfoBlockHandler : IRequestHandler<CreateInfoBlockCommand, Result<InfoBlockDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public CreateInfoBlockHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that creates an infoblock.
        /// </summary>
        /// <param name="request">
        /// Request with new infoblock.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A InfoBlockDto, or error, if it was while creating process.
        /// </returns>
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
