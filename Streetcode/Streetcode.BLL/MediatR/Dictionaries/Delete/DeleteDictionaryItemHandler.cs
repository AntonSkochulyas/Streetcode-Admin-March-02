// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Dictionaries.Delete
{
    /// <summary>
    /// Handler, that handles a process of deleting a dictionary item.
    /// </summary>
    public class DeleteDictionaryItemHandler : IRequestHandler<DeleteDictionaryItemCommand, Result<Unit>>
    {
        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public DeleteDictionaryItemHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that deletes a dictionary item by given id.
        /// </summary>
        /// <param name="request">
        /// Request with dictionary item id to delete.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A Unit, or error, if it was while deleting process.
        /// </returns>
        public async Task<Result<Unit>> Handle(DeleteDictionaryItemCommand request, CancellationToken cancellationToken)
        {
            int id = request.Id;

            var dictionaryItem = await _repositoryWrapper.DictionaryItemRepository.GetFirstOrDefaultAsync(n => n.Id == id);

            if (dictionaryItem == null)
            {
                string errorMsg = $"No dictionary item found by entered Id - {id}";

                _logger.LogError(request, errorMsg);

                return Result.Fail(errorMsg);
            }

            _repositoryWrapper.DictionaryItemRepository.Delete(dictionaryItem);

            var resultISSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultISSuccess)
            {
                return Result.Ok(Unit.Value);
            }
            else
            {
                string errorMsg = "Failed to delete dictionary item";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
