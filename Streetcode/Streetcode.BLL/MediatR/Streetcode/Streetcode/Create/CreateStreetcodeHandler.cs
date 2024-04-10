using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.Create;

public class CreateStreetcodeHandler : IRequestHandler<CreateStreetcodeCommand, Result<StreetcodeDto>>
{
    private readonly IRepositoryWrapper _repository;
    private readonly IMapper _mapper;
    private readonly ILoggerService _logger;

    public CreateStreetcodeHandler(IRepositoryWrapper repository, IMapper mapper, ILoggerService logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<StreetcodeDto>> Handle(CreateStreetcodeCommand request, CancellationToken cancellationToken)
    {
        var streetcode = _mapper.Map<StreetcodeContent>(request.Streetcode);

        if (streetcode is null)
        {
            string errorMsg = StreetcodeErrors.CreateStreetcodeCannotCreateNewStreetcodeError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        var createdStreetcode = _repository.StreetcodeRepository.Create(streetcode);

        var isSuccessResult = await _repository.SaveChangesAsync() > 0;

        if(!isSuccessResult)
        {
            string errorMsg = StreetcodeErrors.CreateRelatedTermHandlerCannotSaveChangesInDatabaseAfterRelatedWordCreationError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        var createdStreetcodeDTO = _mapper.Map<StreetcodeDto>(createdStreetcode);

        if(createdStreetcodeDTO != null)
        {
            return Result.Ok(createdStreetcodeDTO);
        }
        else
        {
            string errorMsg = StreetcodeErrors.CreateRelatedTermHandlerCannotMapEntityError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }
    }
}
