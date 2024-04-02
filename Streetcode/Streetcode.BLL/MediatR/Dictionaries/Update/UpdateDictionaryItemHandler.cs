using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Dictionaries;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Dictionaries.Update
{
    public class UpdateDictionaryItemHandler : IRequestHandler<UpdateDictionaryItemCommand, Result<DictionaryItemDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public UpdateDictionaryItemHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, IBlobService blobSevice, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

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
