using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Dictionaries.GetById
{
    public class GetDictionaryItemByIdHandler : IRequestHandler<GetDictionaryItemByIdQuery, Result<DictionaryItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetDictionaryItemByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

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
