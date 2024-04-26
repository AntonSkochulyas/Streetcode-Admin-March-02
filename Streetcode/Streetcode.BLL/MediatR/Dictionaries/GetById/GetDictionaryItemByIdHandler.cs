// Necessary usings
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.Dictionaries.GetById
{
    /// <summary>
    /// Handler, that handles a process of getting dictionary item by id.
    /// </summary>
    public class GetDictionaryItemByIdHandler : IRequestHandler<GetDictionaryItemByIdQuery, Result<DictionaryItemDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public GetDictionaryItemByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that gets dictionary item by id.
        /// </summary>
        /// <param name="request">
        /// Request to ge ta firctionary item by given id.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A DictionaryItemDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<DictionaryItemDto>> Handle(GetDictionaryItemByIdQuery request, CancellationToken cancellationToken)
        {
            var dictionaryItem = await _repositoryWrapper.DictionaryItemRepository.GetFirstOrDefaultAsync(d => d.Id == request.Id);

            if (dictionaryItem is null)
            {
                string errorMsg = $"Cannot find a dictionary item with corresponding id: {request.Id}";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<DictionaryItemDto>(dictionaryItem));
        }
    }
}
