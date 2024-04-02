using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Dictionaries;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Dictionaries.Create
{
    public class CreateDictionaryItemHandler : IRequestHandler<CreateDictionaryItemCommand, Result<DictionaryItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public CreateDictionaryItemHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<DictionaryItemDto>> Handle(CreateDictionaryItemCommand request, CancellationToken cancellationToken)
        {
            var newDictionaryItem = _mapper.Map<DictionaryItem>(request.newDictionaryItem);

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
