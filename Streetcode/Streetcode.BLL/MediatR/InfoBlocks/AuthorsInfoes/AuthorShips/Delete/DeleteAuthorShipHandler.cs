// Necessary usings
using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Delete
{
    /// <summary>
    /// Handler, that handles a process of deleting a authorship.
    /// </summary>
    public class DeleteAuthorShipHandler : IRequestHandler<DeleteAuthorShipCommand, Result<Unit>>
    {
        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor 
        public DeleteAuthorShipHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that deletes authorship by given id.
        /// </summary>
        /// <param name="request">
        /// Request with authorship id to delete.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A Unit, or error, if it was while deleting process.
        /// </returns>
        public async Task<Result<Unit>> Handle(DeleteAuthorShipCommand request, CancellationToken cancellationToken)
        {
            int id = request.Id;

            var authorShip = await _repositoryWrapper.AuthorShipRepository.GetFirstOrDefaultAsync(a => a.Id == id);

            if (authorShip == null)
            {
                string errorMsg = $"No authorship found by entered Id - {id}";

                _logger.LogError(request, errorMsg);

                return Result.Fail(errorMsg);
            }

            _repositoryWrapper.AuthorShipRepository.Delete(authorShip);

            var resultISSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultISSuccess)
            {
                return Result.Ok(Unit.Value);
            }
            else
            {
                string errorMsg = "Failed to delete authorship";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
