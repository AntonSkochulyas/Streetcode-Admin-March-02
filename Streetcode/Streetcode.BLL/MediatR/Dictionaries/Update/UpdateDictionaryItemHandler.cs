// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Dictionaries;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Dictionaries.Update
{
    /// <summary>
    /// Handler, that handles a process of updating dictionary item.
    /// </summary>
    public class UpdateDictionaryItemHandler : IRequestHandler<UpdateDictionaryItemCommand, Result<DictionaryItemDto>>
    {
        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Mapper
        private readonly IMapper _mapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor 
        public UpdateDictionaryItemHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, IBlobService blobSevice, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that updates a dictionary item.
        /// </summary>
        /// <param name="request">
        /// Request with updated dictionary item.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A DictionaryItemDto, or errorm if it was while updating process.
        /// </returns>
        public async Task<Result<DictionaryItemDto>> Handle(UpdateDictionaryItemCommand request, CancellationToken cancellationToken)
        {
            var dictionaryItem = _mapper.Map<DictionaryItem>(request.dictionaryItem);

            if (dictionaryItem is null)
            {
                const string errorMsg = $"Cannot convert null to dictionary item";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            var response = _mapper.Map<DictionaryItemDto>(dictionaryItem);

            _repositoryWrapper.DictionaryItemRepository.Update(dictionaryItem);

            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(response);
            }
            else
            {
                const string errorMsg = $"Failed to update dictionary item";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
