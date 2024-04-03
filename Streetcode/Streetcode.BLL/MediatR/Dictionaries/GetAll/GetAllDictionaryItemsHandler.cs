using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Dictionaries.GetAll
{
    public class GetAllDictionaryItemsHandler : IRequestHandler<GetAllDictionaryItemsQuery, Result<IEnumerable<DictionaryItemDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetAllDictionaryItemsHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

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
