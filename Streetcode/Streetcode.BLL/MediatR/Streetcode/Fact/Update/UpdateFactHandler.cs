using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent.Fact;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Fact.Update;

public class UpdateFactHandler : IRequestHandler<UpdateFactCommand, Result<FactDto>>
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;
    private readonly ILoggerService _logger;
    public UpdateFactHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<FactDto>> Handle(UpdateFactCommand request, CancellationToken cancellationToken)
    {
        if (await _repositoryWrapper.FactRepository.GetFirstOrDefaultAsync(x => x.Id == request.Fact.Id) is null)
        {
            return LogAndReturnError(string.Format(StreetcodeErrors.UpdateFactHandlerFactWithIdDoesNotExistError, request.Fact.Id), request);
        }

        if (await _repositoryWrapper.StreetcodeRepository.GetFirstOrDefaultAsync(x => x.Id == request.Fact.StreetcodeId) is null)
        {
            return LogAndReturnError(string.Format(StreetcodeErrors.UpdateFactHandlerStreetcodeWithIdDoesNotExistError, request.Fact.StreetcodeId), request);
        }

        if (await _repositoryWrapper.ImageRepository.GetFirstOrDefaultAsync(x => x.Id == request.Fact.ImageId) is null)
        {
            return LogAndReturnError(string.Format(StreetcodeErrors.UpdateFactHandlerImageWithIdDoesNotExistError, request.Fact.ImageId), request);
        }

        var fact = _mapper.Map<DAL.Entities.Streetcode.TextContent.Fact>(request.Fact);

        if (fact is null)
        {
            return LogAndReturnError(StreetcodeErrors.UpdateFactHandlerCannotConvertNullToFactError, request);
        }

        _repositoryWrapper.FactRepository.Update(fact);

        var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;
        if (resultIsSuccess)
        {
            var response = _mapper.Map<FactDto>(fact);
            return Result.Ok(response);
        }
        else
        {
            return LogAndReturnError(StreetcodeErrors.UpdateFactHandlerFailedToUpdateError, request);
        }
    }

    private Result<FactDto> LogAndReturnError(string errorMsg, UpdateFactCommand request)
    {
        _logger.LogError(request, errorMsg);
        return Result.Fail(new Error(errorMsg));
    }
}
