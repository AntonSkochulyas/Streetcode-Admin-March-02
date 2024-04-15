// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.InfoBlockss.Delete
{
    /// <summary>
    /// Handler, that handles a process of deleting an infoblock.
    /// </summary>
    public class DeleteInfoBlockHandler : IRequestHandler<DeleteInfoBlockCommand, Result<Unit>>
    {
        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor 
        public DeleteInfoBlockHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
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
        public async Task<Result<Unit>> Handle(DeleteInfoBlockCommand request, CancellationToken cancellationToken)
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
                return Result.Ok(Unit.Value);
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
