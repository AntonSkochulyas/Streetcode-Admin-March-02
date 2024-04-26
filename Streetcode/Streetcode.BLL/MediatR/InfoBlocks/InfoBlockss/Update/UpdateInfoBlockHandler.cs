// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.InfoBlocks;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Update
{
    /// <summary>
    /// Handler, that handles a process of updating infoblock.
    /// </summary>
    public class UpdateInfoBlockHandler : IRequestHandler<UpdateInfoBlockCommand, Result<InfoBlockDto>>
    {
        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Mapper
        private readonly IMapper _mapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public UpdateInfoBlockHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, IBlobService blobSevice, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that updates an infoblock.
        /// </summary>
        /// <param name="request">
        /// Request with updated infoblock.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A InfoBlockDto, or error, if it was while updating process.
        /// </returns>
        public async Task<Result<InfoBlockDto>> Handle(UpdateInfoBlockCommand request, CancellationToken cancellationToken)
        {
            var infoBlock = _mapper.Map<InfoBlock>(request.InfoBlock);

            if (infoBlock is null)
            {
                const string errorMsg = $"Cannot convert null to info block";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            var response = _mapper.Map<InfoBlockDto>(infoBlock);

            _repositoryWrapper.InfoBlockRepository.Update(infoBlock);

            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(response);
            }
            else
            {
                const string errorMsg = $"Failed to update info block";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
