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
            string errorMsg = "Failed to map dto";
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        string? errorMsg1 = await FindError(request);
        if (errorMsg1 is not null)
        {
            _logger.LogError(request, errorMsg1);
            return Result.Fail(new Error(errorMsg1));
        }

        var createdStreetcode = _repository.StreetcodeRepository.Create(streetcode);

        var isSuccessResult = await _repository.SaveChangesAsync() > 0;

        if(!isSuccessResult)
        {
            string errorMsg = "Failed to save streetcode";
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

    private async Task<string?> FindError(CreateStreetcodeCommand request)
    {
        if (await _repository.StreetcodeRepository.GetFirstOrDefaultAsync(i => i.Index == request.Streetcode.Index) is not null)
        {
            return $"The streetcode with index {request.Streetcode.ImageAnimatedId} already exist";
        }

        if (await _repository.StreetcodeRepository.GetFirstOrDefaultAsync(i => i.TransliterationUrl == request.Streetcode.TransliterationUrl) is not null)
        {
            return $"The streetcode with transliterationUrl {request.Streetcode.TransliterationUrl} already exist";
        }

        if (await _repository.ImageMainRepository.GetFirstOrDefaultAsync(i => i.Id == request.Streetcode.ImageAnimatedId) is null)
        {
            return $"The image with id {request.Streetcode.ImageAnimatedId} does not exist";
        }

        if (await _repository.StreetcodeRepository.GetFirstOrDefaultAsync(s => s.ImageAnimatedId == request.Streetcode.ImageAnimatedId) is not null)
        {
            return $"The image with id {request.Streetcode.ImageAnimatedId} is already used";
        }

        if (await _repository.ImageMainRepository.GetFirstOrDefaultAsync(i => i.Id == request.Streetcode.ImageBlackAndWhiteId) is null)
        {
            return $"The image with id {request.Streetcode.ImageBlackAndWhiteId} does not exist";
        }

        if (await _repository.StreetcodeRepository.GetFirstOrDefaultAsync(s => s.ImageBlackAndWhiteId == request.Streetcode.ImageBlackAndWhiteId) is not null)
        {
            return $"The image with id {request.Streetcode.ImageBlackAndWhiteId} is already used";
        }

        if (await _repository.ImageMainRepository.GetFirstOrDefaultAsync(i => i.Id == request.Streetcode.ImageForLinkId) is null)
        {
            return $"The image with id {request.Streetcode.ImageForLinkId} does not exist";
        }

        if (await _repository.StreetcodeRepository.GetFirstOrDefaultAsync(s => s.ImageForLinkId == request.Streetcode.ImageForLinkId) is not null)
        {
            return $"The image with id {request.Streetcode.ImageForLinkId} is already used";
        }

        return null;
    }
}
