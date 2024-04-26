// Necessary usings
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Dictionaries;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.Dictionaries.Create
{
    /// <summary>
    /// Handler, that handles a process of creating dictionary item.
    /// </summary>
    public class CreateDictionaryItemHandler : IRequestHandler<CreateDictionaryItemCommand, Result<DictionaryItemDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor 
        public CreateDictionaryItemHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that creates a new dictionary item.
        /// </summary>
        /// <param name="request">
        /// Request with new dictionary item inside.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cnacelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A DictionaryItemDto, or error, if it was while creating process.
        /// </returns>
        public async Task<Result<DictionaryItemDto>> Handle(CreateDictionaryItemCommand request, CancellationToken cancellationToken)
        {
            var newDictionaryItem = _mapper.Map<DictionaryItem>(request.CreateDictionaryItemDto);

            if (newDictionaryItem is null)
            {
                const string errorMsg = "Cannot convert null to dictionary item";

                _logger.LogError(request, errorMsg);

                return Result.Fail(errorMsg);
            }

            var entity = _repositoryWrapper.DictionaryItemRepository.Create(newDictionaryItem);

            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(_mapper.Map<DictionaryItemDto>(entity));
            }
            else
            {
                const string errorMsg = "Failed to create a dictionary item";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
