// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Dictionaries.Delete
{
    /// <summary>
    /// Handler, that handles a process of deleting a dictionary item.
    /// </summary>
    public class DeleteDictionaryItemHandler : IRequestHandler<DeleteDictionaryItemCommand, Result<DictionaryItemDto>>
    {
        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        private readonly IMapper _mapper;

        // Parametric constructor
        public DeleteDictionaryItemHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mapper = mapper;
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
        public async Task<Result<DictionaryItemDto>> Handle(DeleteDictionaryItemCommand request, CancellationToken cancellationToken)
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
                return Result.Ok(_mapper.Map<DictionaryItemDto>(dictionaryItem));
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
