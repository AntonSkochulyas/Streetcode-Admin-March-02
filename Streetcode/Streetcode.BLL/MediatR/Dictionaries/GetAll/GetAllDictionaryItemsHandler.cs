// Necessary usings
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespace
namespace Streetcode.BLL.MediatR.Dictionaries.GetAll
{
    /// <summary>
    /// Handler, that handles a get all dictionary items request.
    /// </summary>
    public class GetAllDictionaryItemsHandler : IRequestHandler<GetAllDictionaryItemsQuery, Result<IEnumerable<DictionaryItemDto>>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public GetAllDictionaryItemsHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that get all dictionary items from database.
        /// </summary>
        /// <param name="request">
        /// Request to get all dictionary items.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A IEnumerable of DictionaryItemDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<IEnumerable<DictionaryItemDto>>> Handle(GetAllDictionaryItemsQuery request, CancellationToken cancellationToken)
        {
            var dictionaryItems = await _repositoryWrapper.DictionaryItemRepository.GetAllAsync();

            if (dictionaryItems is null)
            {
                const string errorMsg = $"Cannot find any dictionary items";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<DictionaryItemDto>>(dictionaryItems));
        }
    }
}
