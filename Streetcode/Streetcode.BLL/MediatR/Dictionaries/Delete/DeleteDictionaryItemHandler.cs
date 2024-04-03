using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Dictionaries.Delete
{
    public class DeleteDictionaryItemHandler : IRequestHandler<DeleteDictionaryItemCommand, Result<Unit>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public DeleteDictionaryItemHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

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
