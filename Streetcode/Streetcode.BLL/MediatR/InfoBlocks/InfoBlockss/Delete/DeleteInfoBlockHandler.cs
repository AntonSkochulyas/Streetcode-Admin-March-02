// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Delete
{
    /// <summary>
    /// Handler, that handles a process of deleting an infoblock.
    /// </summary>
    public class DeleteInfoBlockHandler : IRequestHandler<DeleteInfoBlockCommand, Result<InfoBlockDto>>
    {
        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Mapper
        private readonly IMapper _mapper;

        // Parametric constructor 
        public DeleteInfoBlockHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Method, that deletes an infoblock.
        /// </summary>
        /// <param name="request">
        /// Infoblock id to delete.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A Unit, or error, if it was while deleting process.
        /// </returns>
        public async Task<Result<InfoBlockDto>> Handle(DeleteInfoBlockCommand request, CancellationToken cancellationToken)
        {
            int id = request.Id;

            var infoBlock = await _repositoryWrapper.InfoBlockRepository.GetFirstOrDefaultAsync(n => n.Id == id);

            if (infoBlock == null)
            {
                string errorMsg = $"No info block found by entered Id - {id}";

                _logger.LogError(request, errorMsg);

                return Result.Fail(errorMsg);
            }

            _repositoryWrapper.InfoBlockRepository.Delete(infoBlock);

            var resultISSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultISSuccess)
            {
                return Result.Ok(_mapper.Map<InfoBlockDto>(infoBlock));
            }
            else
            {
                string errorMsg = "Failed to delete info block";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
