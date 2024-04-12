﻿// Necessary usings
using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.Delete
{
    /// <summary>
    /// Handler, that handles a process of deleting an authorship hyperlink.
    /// </summary>
    public class DeleteAuthorShipHyperLinkHandler : IRequestHandler<DeleteAuthorShipHyperLinkCommand, Result<Unit>>
    {
        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public DeleteAuthorShipHyperLinkHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that deletes an authorship hyperlink by given id.
        /// </summary>
        /// <param name="request">
        /// Request with authorship hyperlink id to delete.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A Unit, or error, if it was while deleting process.
        /// </returns>
        public async Task<Result<Unit>> Handle(DeleteAuthorShipHyperLinkCommand request, CancellationToken cancellationToken)
        {
            int id = request.Id;

            var authorsHyperLink = await _repositoryWrapper.AuthorShipHyperLinkRepository.GetFirstOrDefaultAsync(a => a.Id == id);

            if (authorsHyperLink == null)
            {
                string errorMsg = $"No authors hyper link found by entered Id - {id}";

                _logger.LogError(request, errorMsg);

                return Result.Fail(errorMsg);
            }

            _repositoryWrapper.AuthorShipHyperLinkRepository.Delete(authorsHyperLink);

            var resultISSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultISSuccess)
            {
                return Result.Ok(Unit.Value);
            }
            else
            {
                string errorMsg = "Failed to delete authors hyper link";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
